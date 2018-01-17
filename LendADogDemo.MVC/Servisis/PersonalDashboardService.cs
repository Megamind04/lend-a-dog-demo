﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LendADogDemo.Entities.Interfaces;
using LendADogDemo.Entities.UoW;
using LendADogDemo.MVC.ViewModels;
using LendADogDemo.Entities.Models;
using Ninject;

namespace LendADogDemo.MVC.Servisis
{
    public class PersonalDashboardService : IPersonalDashboardService
    {
        private IUnitOfWork _unitOfWork;

        private IPrivateMessageBoardRepository _privaMessRepo;
        private IDogRepository _dogRepo;
        private IDogPhotoRepository _dogPhotoRepo;
        private IRequestMessageRepository _reqMessRepo;
        private IDogOwnerRepository _dogOwnerRepo;

        public PersonalDashboardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _privaMessRepo = _unitOfWork.PrivateMessRepo;
            _dogRepo = _unitOfWork.DogRepo;
            _dogPhotoRepo = _unitOfWork.DogPhotoRepo;
            _reqMessRepo = _unitOfWork.RequestMessRepo;
            _dogOwnerRepo = _unitOfWork.DogOwnerRepo;
        }

        public PersonalDashboardViewModel GetMyPersonalDashboardModel(string userId)
        {

            PersonalDashboardViewModel personalDashboardViewModel = new PersonalDashboardViewModel()
            {
                NotConfirmedUsersRequests = GetConfirmations(userId),
                Conversations = GetConversations(userId),
                Dogs = GetDogsToDisplay(userId)
            };

            return personalDashboardViewModel;

        }

        private List<DogViewModel> GetDogsToDisplay(string userId)
            => _dogRepo.GetDogWithPhotos(userId).Select(x => new DogViewModel()
            {
                DogID = x.DogID,
                DogOwnerID = x.DogOwnerID,
                DogName = x.DogName,
                DogSize = x.DogSize,
                Description = x.Description,
                Photo = x.DogPhotos.Select(p => new DogPhotoViewModel()
                {
                    DogID = p.DogID,
                    Photo = p.Photo
                }).LastOrDefault()
            }).ToList();

        private List<ConversationViewModel> GetConversations(string userId)
            => _privaMessRepo.GetByDogOwnerId(userId)
                .GroupBy(x => x, new ConversationComparer())
                .Select(g => g.Last())
                .Select(m => new ConversationViewModel()
                {
                    LastMessage = m.Message,
                    OtherFullName = m.SenderID == userId ? m.ReceiverDogOwner.FullName : m.SenderDogOwner.FullName,
                    OtherID = m.SenderID == userId ? m.ReceiverID : m.SenderID
                }).ToList();

        private List<NotConfirmedUsersRequestViewModel> GetConfirmations(string userId)
            => _reqMessRepo.GetWithDogOwnerSender(userId)
                .Select(x => x.SendRequestMessage.IsConfirmed == true ? null : new NotConfirmedUsersRequestViewModel()
                {
                    RequestFromID = x.SendFromID,
                    Message = x.Message,
                    RequestFromFullName = x.SendRequestMessage.FullName
                }).ToList();

        //List<NotConfirmedUsersRequestViewModel> confirmations = new List<NotConfirmedUsersRequestViewModel>();
        //foreach (var item in listRequests)
        //{
        //    if (item.DogOwner.IsConfirmed == false)
        //    {
        //        confirmations.Add(new NotConfirmedUsersRequestViewModel
        //        {
        //            RequestFromID = item.DogOwner.Id,
        //            Message = item.Message,
        //            RequestFromFullName = item.DogOwner.FirstName + " " + item.DogOwner.LastName
        //        });
        //    }
        //}
        //return listRequests;

    }

    class ConversationComparer : IEqualityComparer<PrivateMessageBoard>
    {
        public bool Equals(PrivateMessageBoard x, PrivateMessageBoard y)
        {
            return (x.SenderID == y.ReceiverID && x.ReceiverID == y.SenderID) ||
                (x.SenderID == y.SenderID && x.ReceiverID == y.ReceiverID);
        }

        public int GetHashCode(PrivateMessageBoard obj)
        {
            return obj.SenderID.GetHashCode() ^ obj.ReceiverID.GetHashCode();
        }
    }
}