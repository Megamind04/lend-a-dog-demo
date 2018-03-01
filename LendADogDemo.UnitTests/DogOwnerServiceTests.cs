using LendADogDemo.Entities.Interfaces;
using LendADogDemo.Entities.Models;
using LendADogDemo.Entities.UoW;
using LendADogDemo.MVC.Servisis;
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
                IsConfirmed = false
            };

            appUser = new DogOwner
            {
                Id = "UserID",
                IsConfirmed = true
            };

            list = new List<RequestMessage>()
            {
                new RequestMessage {Message="msg1",SendFromID = "ID",ReciverID = "NotValidID"},
                new RequestMessage {Message="msg2",SendFromID = "ID",ReciverID = "UserID"},
                new RequestMessage {Message="msg3",SendFromID = "ID",ReciverID = "NotValidID"},
                new RequestMessage {Message="msg4",SendFromID = "NotValidID",ReciverID = "NotValidID"}
            };
        }

        [Test]
        public void ShouldVerifyUserIfUserIsNotConfirmed()
        {  
            mockDogOwnerRepo.Setup(x => x.GetByID(It.IsAny<string>())).Returns(UserToVerifyOrDeny);

            mockRequestMessageRepo.Setup(x => x.Get(It.IsAny<Expression<Func<RequestMessage, bool>>>(),
                It.IsAny<Func<IQueryable<RequestMessage>, IOrderedQueryable<RequestMessage>>>(), It.IsAny<string>()))
                .Returns(list.Where(l=>l.SendFromID == UserToVerifyOrDeny.Id));

            mockRequestMessageRepo.Setup(x => x.Delete(It
                .Is<RequestMessage>(m=>m.SendFromID == UserToVerifyOrDeny.Id)))
                .Callback(()=>list.RemoveAt(0));

            IDogOwnerService dogService = new DogOwnerService(mockUnitOfWork.Object,mockDogOwnerRepo.Object,
                mockRequestMessageRepo.Object,mockPrivateMessageBoardRepo.Object);

            dogService.VerifyUser(UserToVerifyOrDeny.Id);

            Assert.That(UserToVerifyOrDeny.IsConfirmed, Is.True);
            Assert.That(list.Count, Is.EqualTo(1));
        }

        [Test]
        public void ShouldDeleteRequestMessageOnDeny()
        {            
            IEnumerable<RequestMessage> msgToDelete = list.Where(l => l.SendFromID == UserToVerifyOrDeny.Id && l.ReciverID == appUser.Id);

            mockRequestMessageRepo.Setup(x => x.Get(
                It.IsAny<Expression<Func<RequestMessage, bool>>>(),
                It.IsAny<Func<IQueryable<RequestMessage>, IOrderedQueryable<RequestMessage>>>(), It.IsAny<string>()))
                .Returns(msgToDelete);

            mockRequestMessageRepo.Setup(x => x.Delete(It
                .Is<RequestMessage>(m => m.SendFromID == UserToVerifyOrDeny.Id && m.ReciverID == appUser.Id)))
                .Callback(() => list.Remove(msgToDelete.FirstOrDefault()));

            IDogOwnerService dogService = new DogOwnerService(mockUnitOfWork.Object, mockDogOwnerRepo.Object,
                mockRequestMessageRepo.Object, mockPrivateMessageBoardRepo.Object);

            dogService.DenyUser(appUser.Id, UserToVerifyOrDeny.Id);

            Assert.That(UserToVerifyOrDeny.IsConfirmed, Is.False);
            Assert.That(list.Count, Is.EqualTo(3));
        }
    }
}
