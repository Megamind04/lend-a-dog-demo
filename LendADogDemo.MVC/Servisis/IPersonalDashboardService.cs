using System;
using System.Collections.Generic;
using System.Linq;
using LendADogDemo.Entities.Interfaces;
using LendADogDemo.Entities.UoW;
using LendADogDemo.MVC.ViewModels;
using LendADogDemo.Entities.Models;
using System.Diagnostics;
using LendADogDemo.Entities.Helpers;

namespace LendADogDemo.MVC.Servisis
{
    public interface IPersonalDashboardService
    {
        byte[] GetLastImage(int dogId);

        IEnumerable<IEnumerable<DogViewModel>> GetDogsPerUser(string userId);

        IEnumerable<ConversationViewModel> GetConversationsPerUser(string userId);

        IEnumerable<NotConfirmedUsersRequestViewModel> GetConfirmationsPerUser(string userId);
    }

    public class PersonalDashboardService : IPersonalDashboardService
    {

        #region Initialize

        private readonly IUnitOfWork unitOfWork;
        private readonly IDogRepository dogRepo;
        private readonly IPrivateMessageBoardRepository privateMessageBoardRepo;
        private readonly IRequestMessageRepository requestMessageRepo;
        private readonly IDogPhotoRepository dogPhotoRepo;

        public PersonalDashboardService(IUnitOfWork _unitOfWork, IDogRepository _dogRepo,
            IPrivateMessageBoardRepository _privateMessageBoardRepo, IRequestMessageRepository _requestMessageRepo,
            IDogPhotoRepository _dogPhotoRepo)
        {
            unitOfWork = _unitOfWork;
            dogRepo = _dogRepo;
            privateMessageBoardRepo = _privateMessageBoardRepo;
            requestMessageRepo = _requestMessageRepo;
            dogPhotoRepo = _dogPhotoRepo;
        }

        #endregion

        public byte[] GetLastImage(int dogId)
        {
            try
            {
                return dogPhotoRepo.Get(filter: x => x.DogID == dogId).LastOrDefault().Photo;
            }
            catch (NullReferenceException ex)
            {
                Debug.Write(ex.Message);
                return null;
            }

        }

        public IEnumerable<IEnumerable<DogViewModel>> GetDogsPerUser(string userId)
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
                    LastDogPhoto = Convert.ToBase64String(d.DogPhotos.LastOrDefault().Photo)

                }));
        }

        public IEnumerable<ConversationViewModel> GetConversationsPerUser(string userId)
        {
            return privateMessageBoardRepo.GetByDogOwnerId(userId)
                       .GroupBy(x => x, new ConversationComparer())
                       .Select(g => g.Last())
                       .Select(m => new ConversationViewModel()
                       {
                           LastMessage = m.Message,
                           OtherFullName = m.SendFromID == userId ? m.ReceiverOfPrivateMessage.FullName : m.SenderOfPrivateMessage.FullName,
                           OtherID = m.SendFromID == userId ? m.RrecivedFromID : m.SendFromID
                       });
        }

        public IEnumerable<NotConfirmedUsersRequestViewModel> GetConfirmationsPerUser(string userId)
        {
            return requestMessageRepo.GetUnconfirmedRequests(userId)
                           .Select(x => new NotConfirmedUsersRequestViewModel()
                           {
                               RequestFromID = x.SendFromID,
                               Message = x.Message,
                               RequestFromFullName = x.SenderOfRequest.FullName
                           });
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