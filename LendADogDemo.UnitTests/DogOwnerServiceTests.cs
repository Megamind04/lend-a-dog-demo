using LendADogDemo.Entities.Interfaces;
using LendADogDemo.Entities.Models;
using LendADogDemo.Entities.UoW;
using LendADogDemo.MVC.Services;
using LendADogDemo.MVC.ViewModels;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LendADogDemo.UnitTests
{
    [TestFixture]
    public class DogOwnerServiceTests
    {
        #region setUp

        Mock<IUnitOfWork> mockUnitOfWork;
        Mock<IDogOwnerRepository> mockDogOwnerRepo;
        Mock<IRequestMessageRepository> mockRequestMessageRepo;
        Mock<IPrivateMessageBoardRepository> mockPrivateMessageBoardRepo;
        List<RequestMessage> list;
        DogOwner UserToVerifyOrDeny;
        DogOwner appUser;

        [SetUp]
        public void SetUp()
        {
            mockUnitOfWork = new Mock<IUnitOfWork>();
            mockDogOwnerRepo = new Mock<IDogOwnerRepository>();
            mockRequestMessageRepo = new Mock<IRequestMessageRepository>();
            mockPrivateMessageBoardRepo = new Mock<IPrivateMessageBoardRepository>();

            UserToVerifyOrDeny = new DogOwner
            {
                Id = "ID",
                IsConfirmed = false,
                FirstName = "UserToVerifyOrDeny",
                LastName = "Something"
            };

            appUser = new DogOwner
            {
                Id = "UserID",
                IsConfirmed = true,
                FirstName = "appUser",
                LastName = "Something"
            };

            list = new List<RequestMessage>()
            {
                new RequestMessage {Message="msg1",SendFromID = "ID",ReciverID = "NotValidID"},
                new RequestMessage {Message="msg2",SendFromID = "ID",ReciverID = "UserID"},
                new RequestMessage {Message="msg3",SendFromID = "ID",ReciverID = "NotValidID"},
                new RequestMessage {Message="msg4",SendFromID = "NotValidID",ReciverID = "NotValidID"}
            };
        }

        public IDogOwnerService InitializeNewService()
        {
            return new DogOwnerService(mockUnitOfWork.Object, mockDogOwnerRepo.Object,
                mockRequestMessageRepo.Object, mockPrivateMessageBoardRepo.Object);
        }

        #endregion

        [Test]
        public void ShouldVerifyUserIfUserIsNotConfirmed()
        {
            //Arrange:
            mockDogOwnerRepo.Setup(x => x.GetByID(It.IsAny<string>())).Returns(UserToVerifyOrDeny).Verifiable();

            mockRequestMessageRepo.Setup(x => x.Get(
                It.IsAny<Expression<Func<RequestMessage, bool>>>(),
                It.IsAny<Func<IQueryable<RequestMessage>, IOrderedQueryable<RequestMessage>>>(), It.IsAny<string>()))
                .Returns(list.Where(l => l.SendFromID == UserToVerifyOrDeny.Id)).Verifiable();

            mockRequestMessageRepo.Setup(x => x.Delete(It
                .Is<RequestMessage>(m => m.SendFromID == UserToVerifyOrDeny.Id)))
                .Callback(() => list.RemoveAt(0)).Verifiable();
            //-----------//

            //Act:
            IDogOwnerService dogService = InitializeNewService();
            bool isItTrue = dogService.VerifyUser(UserToVerifyOrDeny.Id);
            //-----------//

            //Assert:
            mockDogOwnerRepo.Verify(mock => mock.GetByID(It.IsAny<string>()), Times.Once);
            mockDogOwnerRepo.Verify(Mock => Mock.Update(It.IsAny<DogOwner>()), Times.Once);

            mockRequestMessageRepo.Verify(mock => mock.Get(
                It.IsAny<Expression<Func<RequestMessage, bool>>>(),
                It.IsAny<Func<IQueryable<RequestMessage>, IOrderedQueryable<RequestMessage>>>(), It.IsAny<string>()), Times.Once);

            mockRequestMessageRepo.Verify(mock => mock.Delete(It.IsAny<RequestMessage>()), Times.Exactly(3));
            mockUnitOfWork.Verify(mock => mock.Commit(), Times.Exactly(2));

            Assert.That(UserToVerifyOrDeny.IsConfirmed, Is.True);
            Assert.That(list.Count, Is.EqualTo(1));
            Assert.That(isItTrue, Is.True);
            //-----------//
        }

        [Test]
        public void ShouldDeleteRequestMessageOnDeny()
        {
            //Arrange:
            IEnumerable<RequestMessage> msgToDelete = list.Where(l => l.SendFromID == UserToVerifyOrDeny.Id && l.ReciverID == appUser.Id);

            mockRequestMessageRepo.Setup(x => x.Get(
                It.IsAny<Expression<Func<RequestMessage, bool>>>(),
                It.IsAny<Func<IQueryable<RequestMessage>, IOrderedQueryable<RequestMessage>>>(), It.IsAny<string>()))
                .Returns(msgToDelete).Verifiable();

            mockRequestMessageRepo.Setup(x => x.Delete(
                It.Is<RequestMessage>(m => m.SendFromID == UserToVerifyOrDeny.Id && m.ReciverID == appUser.Id)))
                .Callback(() => list.Remove(msgToDelete.FirstOrDefault())).Verifiable();
            //-----------//

            //Act:
            IDogOwnerService dogService = InitializeNewService();
            bool isItTrue = dogService.DenyUser(appUser.Id, UserToVerifyOrDeny.Id);
            //-----------//

            //Assert:
            mockRequestMessageRepo.Verify(mock => mock.Get(
                It.IsAny<Expression<Func<RequestMessage, bool>>>(),
                It.IsAny<Func<IQueryable<RequestMessage>, IOrderedQueryable<RequestMessage>>>(), It.IsAny<string>()), Times.Once);

            mockRequestMessageRepo.Verify(mock => mock.Delete(
                It.IsAny<RequestMessage>()), Times.Once);

            mockUnitOfWork.Verify(mock => mock.Commit(), Times.Once);

            Assert.That(UserToVerifyOrDeny.IsConfirmed, Is.False);
            Assert.That(list.Count, Is.EqualTo(3));
            Assert.That(isItTrue, Is.True);
            //-----------//
        }

        [Test]
        public void ShouldCreateNewCoversationOnSend()
        {
            //Arrange:
            List<PrivateMessageBoard> conversations = new List<PrivateMessageBoard>();
            ConversationViewModel conversation = new ConversationViewModel { LastMessage = "Something", OtherID = "OtherID", };

            mockPrivateMessageBoardRepo.Setup(x => x.Insert(
                It.Is<PrivateMessageBoard>(m => m.Message == "Something")))
                .Callback(() => conversations.Add(new PrivateMessageBoard { Message = "Something" }));
            //-----------//

            //Act:
            IDogOwnerService dogService = InitializeNewService();
            bool isItTrue = dogService.AddConversation(appUser.Id, conversation);
            //-----------//

            //Assert:
            mockPrivateMessageBoardRepo.Verify(mock => mock.Insert(
                It.IsAny<PrivateMessageBoard>()), Times.Once);

            mockUnitOfWork.Verify(mock => mock.Commit(), Times.Once);

            Assert.That(conversations.Count, Is.EqualTo(1));
            Assert.That(isItTrue, Is.True);
            //-----------//
        }

        [Test]
        public void ShouldReturnOnlyUnverifiedUsers()
        {
            //Arrange:
            mockDogOwnerRepo.Setup(x => x.GetByID(It.Is<string>(u => u == UserToVerifyOrDeny.Id))).Returns(UserToVerifyOrDeny).Verifiable();
            //-----------//

            //Act:
            IDogOwnerService dogService = InitializeNewService();
            var user = dogService.GetUnverifiedDogOwner(UserToVerifyOrDeny.Id);
            //-----------//

            //Assert:
            mockDogOwnerRepo.Verify(mock => mock.GetByID(It.IsAny<string>()), Times.Once);
            Assert.That(user, Is.TypeOf(typeof(NotConfirmedUsersRequestViewModel)));
            Assert.That(user.RequestFromID, Is.EqualTo(UserToVerifyOrDeny.Id));
            //-----------//
        }

        [Test]
        public void ShouldReturnOnlyConversationsBetweenTwoUsers()
        {
            //Arrange:
            IEnumerable<PrivateMessageBoard> conversations = new List<PrivateMessageBoard>
            {
                new PrivateMessageBoard{Message = "Something" , RrecivedFromID = "UserID" , SendFromID = "ID",
                    SenderOfPrivateMessage = appUser,ReceiverOfPrivateMessage=UserToVerifyOrDeny},
                new PrivateMessageBoard{Message = "Something" , RrecivedFromID = "ID" , SendFromID = "UserID",
                    SenderOfPrivateMessage = UserToVerifyOrDeny,ReceiverOfPrivateMessage=appUser},
                new PrivateMessageBoard{Message = "Something" , RrecivedFromID = "UserID" , SendFromID = "ID",
                    SenderOfPrivateMessage = appUser,ReceiverOfPrivateMessage=UserToVerifyOrDeny},
                new PrivateMessageBoard{Message = "Something" , RrecivedFromID = "ID" , SendFromID = "UserID",
                    SenderOfPrivateMessage = UserToVerifyOrDeny,ReceiverOfPrivateMessage=appUser},
                new PrivateMessageBoard{Message = "Something" , RrecivedFromID = "NotValidID" , SendFromID = "NotValidID",
                    SenderOfPrivateMessage = UserToVerifyOrDeny,ReceiverOfPrivateMessage=appUser},
            };

            mockPrivateMessageBoardRepo.Setup(x => x.GetMessagesBetweenTwoUsers(
                It.Is<string>(u => u == appUser.Id || u == UserToVerifyOrDeny.Id),
                It.Is<string>(u => u == appUser.Id || u == UserToVerifyOrDeny.Id)))
                .Returns(conversations.Where
                (x => (x.RrecivedFromID == appUser.Id && x.SendFromID == UserToVerifyOrDeny.Id)
                || (x.SendFromID == appUser.Id && x.RrecivedFromID == UserToVerifyOrDeny.Id))).Verifiable();

            //-----------//

            //Act:
            IDogOwnerService dogService = InitializeNewService();
            var result = dogService.ConversationBetweenTwoUsers(appUser.Id, UserToVerifyOrDeny.Id);
            var conversationsResult = result.Conversations.ToList();
            //-----------//

            //Assert
            mockPrivateMessageBoardRepo.Verify(mock => mock.GetMessagesBetweenTwoUsers(
                It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.That(result, Is.TypeOf(typeof(PrivateConversationBetweenTwoUsers)));
            Assert.That(conversationsResult.Count, Is.EqualTo(4));
            //-----------//
        }
    }
}
