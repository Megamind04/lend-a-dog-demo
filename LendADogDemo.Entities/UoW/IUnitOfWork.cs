using System;
using LendADogDemo.Entities.Interfaces;
using LendADogDemo.Entities.Models;


namespace LendADogDemo.Entities.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        IDogOwnerRepository DogOwnerRepo { get; }
        IDogRepository DogRepo { get; }
        IDogPhotoRepository DogPhotoRepo { get; }
        IMainMessageBoardRepository MainMessRepo { get; }
        IPrivateMessageBoardRepository PrivateMessRepo { get; }
        IRequestMessageRepository RequestMessRepo { get; }

        void Save();
    }
}
