using System;
using LendADogDemo.Entities.Interfaces;
using LendADogDemo.Entities.DataContexts;
using LendADogDemo.Entities.Models;
using LendADogDemo.Entities.Infrastructure;

namespace LendADogDemo.Entities.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private LendADogDemoDb context = new LendADogDemoDb();

        private IDogOwnerRepository dogOwnerRepo;
        private IDogRepository dogRepo;
        private IDogPhotoRepository dogPhotoRepo;
        private IMainMessageBoardRepository mainMessRepo;
        private IPrivateMessageBoardRepository privateMessRepo;
        private IRequestMessageRepository requestMessRepo;

        public IDogOwnerRepository DogOwnerRepo
        {
            get
            {
                if (this.dogOwnerRepo == null)
                {
                    this.dogOwnerRepo = new DogOwnerRepository(context);
                }
                return dogOwnerRepo;
            }
        }

        public IDogRepository DogRepo
        {
            get
            {
                if(this.dogRepo == null)
                {
                    this.dogRepo = new DogRepository(context);
                }
                return dogRepo;
            }
        }

        public IDogPhotoRepository DogPhotoRepo
        {
            get
            {
                if(this.dogPhotoRepo == null)
                {
                    this.dogPhotoRepo = new DogPhotoRepository(context);
                }
                return dogPhotoRepo;
            }
        }

        public IMainMessageBoardRepository MainMessRepo
        {
            get
            {
                if (this.mainMessRepo == null)
                {
                    this.mainMessRepo = new MainMessageBoardRepository(context);
                }
                return mainMessRepo;
            }
        }

        public IPrivateMessageBoardRepository PrivateMessRepo
        {
            get
            {
                if (this.privateMessRepo == null)
                {
                    this.privateMessRepo = new PrivateMessageBoardRepository(context);
                }
                return privateMessRepo;
            }
        }

        public IRequestMessageRepository RequestMessRepo
        {
            get
            {
                if (this.requestMessRepo == null)
                {
                    this.requestMessRepo = new RequestMessageRepository(context);
                }
                return requestMessRepo;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
