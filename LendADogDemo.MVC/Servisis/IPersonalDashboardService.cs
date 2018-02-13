using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LendADogDemo.Entities.Interfaces;
using LendADogDemo.Entities.UoW;
using LendADogDemo.MVC.ViewModels;
using LendADogDemo.Entities.Models;
using System.Drawing;
using System.IO;
using Ninject;
using System.Diagnostics;
using System.Data.Entity.Core;
using LendADogDemo.Entities.Helpers;

namespace LendADogDemo.MVC.Servisis
{
    public interface IPersonalDashboardService
    {
        PersonalDashboardViewModel GetMyPersonalDashboardModel(string userId);

        byte[] GetLastImage(int dogId);

        bool CreatePrivetMessage(PrivateMessageBoardViewModel newPrivetMess, string SenderID);

        List<List<DogViewModel>> GetDogsPerUser(string userId);
    }

    public class PersonalDashboardService : IPersonalDashboardService
    {

        #region Initialize

        private readonly IUnitOfWork unitOfWork;
        private readonly IDogRepository dogRepo;
        private readonly IPrivateMessageBoardRepository privateMessageBoardRepo;
        private readonly IRequestMessageRepository requestMessageRepo;
        private readonly IDogPhotoRepository dogPhotoRepo;

        public PersonalDashboardService(IUnitOfWork _unitOfWork, IDogRepository _dogRepo, IPrivateMessageBoardRepository _privateMessageBoardRepo, IRequestMessageRepository _requestMessageRepo, IDogPhotoRepository _dogPhotoRepo)
        {
            unitOfWork = _unitOfWork;
            dogRepo = _dogRepo;
            privateMessageBoardRepo = _privateMessageBoardRepo;
            requestMessageRepo = _requestMessageRepo;
            dogPhotoRepo = _dogPhotoRepo;
        }

        #endregion

        public List<List<DogViewModel>> GetDogsPerUser(string userId)
        {
            var dogsToDisplay = dogRepo.GetDogPerOwner(userId).ToList().SplitList(3);

            return dogsToDisplay
                .Select(x => x
                .Select(d => new DogViewModel()
                {
                    DogID = d.DogID,
                    DogOwnerID = d.DogOwnerID,
                    DogName = d.DogName,
                    DogSize = d.DogSize,
                    Description = d.Description,

                })
                .ToList())
                .ToList();
        }

        public PersonalDashboardViewModel GetMyPersonalDashboardModel(string userId)
        {

            #region Geting Information for personalDashboardViewModel
            IEnumerable<DogViewModel> DogsToDisplay()
            {
                return dogRepo.GetDogPerOwner(userId).Select(x => new DogViewModel()
                {
                    DogID = x.DogID,
                    DogOwnerID = x.DogOwnerID,
                    DogName = x.DogName,
                    DogSize = x.DogSize,
                    Description = x.Description,
                });
            }

            IEnumerable<ConversationViewModel> Conversations()
            {
                return privateMessageBoardRepo.GetByDogOwnerId(userId)
                           .GroupBy(x => x, new ConversationComparer())
                           .Select(g => g.Last())
                           .Select(m => new ConversationViewModel()
                           {
                               LastMessage = m.Message,
                               OtherFullName = m.SendFromID == userId ? m.ReceiverOfPrivateMessage.FullName : m.SenderOfPrivateMessage.FullName,
                               OtherID = m.SendFromID == userId ? m.RrecivedFromID : m.SendFromID
                           }).ToList();
            }

            IEnumerable<NotConfirmedUsersRequestViewModel> Confirmations()
            {
                return requestMessageRepo.GetUnconfirmedRequests(userId)
                           .Select(x => new NotConfirmedUsersRequestViewModel()
                           {
                               RequestFromID = x.SendFromID,
                               Message = x.Message,
                               RequestFromFullName = x.SenderOfRequest.FullName
                           }).ToList();
            }
            #endregion

            PersonalDashboardViewModel personalDashboardViewModel = new PersonalDashboardViewModel()
            {
                Dogs = DogsToDisplay(),
                Conversations = Conversations(),
                NotConfirmedUsersRequests = Confirmations()
            };

            return personalDashboardViewModel;
        }

        public byte[] GetLastImage(int dogId)
        {
            return dogPhotoRepo.Get(filter: x => x.DogID == dogId).LastOrDefault().Photo;
        }

        public bool CreatePrivetMessage(PrivateMessageBoardViewModel newPrivetMess, string SenderID)
        {
            PrivateMessageBoard messageToInser = new PrivateMessageBoard()
            {
                CreateDate = DateTime.Now,
                Message = newPrivetMess.Message,
                SendFromID = SenderID,
                RrecivedFromID = newPrivetMess.RrecivedFromID
            };
            try
            {
                privateMessageBoardRepo.Insert(messageToInser);
                unitOfWork.Commit();
                return true;
            }
            catch (EntityException ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (unitOfWork != null)
                {
                    unitOfWork.Dispose();
                }
            }
        }

    }

    class ConversationComparer : IEqualityComparer<PrivateMessageBoard>
    {
        public bool Equals(PrivateMessageBoard x, PrivateMessageBoard y)
        {
            return (x.SendFromID == y.RrecivedFromID && x.RrecivedFromID == y.SendFromID) ||
                (x.SendFromID == y.SendFromID && x.RrecivedFromID == y.RrecivedFromID);
        }

        public int GetHashCode(PrivateMessageBoard obj)
        {
            return obj.SendFromID.GetHashCode() ^ obj.RrecivedFromID.GetHashCode();
        }
    }
}