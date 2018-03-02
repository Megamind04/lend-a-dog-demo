using LendADogDemo.Entities.Interfaces;
using LendADogDemo.Entities.UoW;
using LendADogDemo.MVC.ViewModels;
using System;
using System.Data.Entity.Core;
using System.Diagnostics;
using System.Linq;
using LendADogDemo.Entities.Models;

namespace LendADogDemo.MVC.Services
{
    public interface IDogOwnerService
    {
        NotConfirmedUsersRequestViewModel GetUnverifiedDogOwner(string UserID);

        bool VerifyUser(string RequestFromID);

        bool DenyUser(string ReciverID, string RequestFromID);

        PrivateConversationBetweenTwoUsers ConversationBetweenTwoUsers(string userId, string otherId);

        bool AddConversation(string userId, ConversationViewModel conversation);
    }

    public class DogOwnerService : IDogOwnerService
    {
        #region Initialize

        private readonly IUnitOfWork unitOfWork;
        private readonly IDogOwnerRepository dogOwnerRepo;
        private readonly IRequestMessageRepository requestMessageRepo;
        private readonly IPrivateMessageBoardRepository privateMessageBoardRepo;

        public DogOwnerService(IUnitOfWork _unitOfWork, IDogOwnerRepository _dogOwnerRepo,IRequestMessageRepository _requestMessageRepo,
            IPrivateMessageBoardRepository _privateMessageBoardRepo)
        {
            unitOfWork = _unitOfWork;
            dogOwnerRepo = _dogOwnerRepo;
            requestMessageRepo = _requestMessageRepo;
            privateMessageBoardRepo = _privateMessageBoardRepo;
        }

        #endregion

        public NotConfirmedUsersRequestViewModel GetUnverifiedDogOwner(string UserID)
        {
            var user = dogOwnerRepo.GetByID(UserID);
            var userTObeApprovOrno = new NotConfirmedUsersRequestViewModel()
            {
                RequestFromID = user.Id,
                RequestFromFullName = user.FullName
            };
            return userTObeApprovOrno;
        }

        public bool VerifyUser(string RequestFromID)
        {
            try
            {
                var usertTobeVerify = dogOwnerRepo.GetByID(RequestFromID);
                if (usertTobeVerify.IsConfirmed)
                {
                    return true;
                }
                else
                {
                    usertTobeVerify.IsConfirmed = true;
                    dogOwnerRepo.Update(usertTobeVerify);
                    unitOfWork.Commit();

                    var requestMsgs = requestMessageRepo.Get(filter: r => r.SendFromID == RequestFromID).ToList();

                    foreach (var msg in requestMsgs)
                    {
                        requestMessageRepo.Delete(msg);
                    }
                    unitOfWork.Commit();

                    return true;
                }
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

        public bool DenyUser(string ReciverID, string RequestFromID)
        {
            try
            {
                var requestMsg = requestMessageRepo.Get(filter: r => r.ReciverID == ReciverID && r.SendFromID == RequestFromID).FirstOrDefault();
                requestMessageRepo.Delete(requestMsg);
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

        public PrivateConversationBetweenTwoUsers ConversationBetweenTwoUsers(string userId, string otherId)
        {
            var conversations = privateMessageBoardRepo.GetMessagesBetweenTwoUsers(userId, otherId)
                .Select(u => new ConversationViewModel()
                {
                    OtherID = u.SendFromID == otherId ? otherId : null,
                    OtherFullName = u.SendFromID == otherId ? u.SenderOfPrivateMessage.FullName : null,
                    LastMessage = u.Message,
                    DateTime = u.CreateDate 
                });
            PrivateConversationBetweenTwoUsers privateConversation = new PrivateConversationBetweenTwoUsers()
            {
                Conversations = conversations,
                NewConversation = new ConversationViewModel()
                {
                    OtherID = otherId,
                    LastMessage = string.Empty
                }
            };
            return privateConversation;
        }

        public bool AddConversation(string userId, ConversationViewModel conversation)
        {
            try
            {
                PrivateMessageBoard NewPrivateMsg = new PrivateMessageBoard()
                {
                    CreateDate = DateTime.Now,
                    Message = conversation.LastMessage,
                    SendFromID = userId,
                    RrecivedFromID = conversation.OtherID
                };
                privateMessageBoardRepo.Insert(NewPrivateMsg);
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
}
