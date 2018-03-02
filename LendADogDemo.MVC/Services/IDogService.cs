using LendADogDemo.Entities.Interfaces;
using LendADogDemo.Entities.UoW;
using LendADogDemo.MVC.ViewModels;
using LendADogDemo.Entities.Models;
using System.Web;
using System.Data.Entity.Core;
using System.Diagnostics;
using LendADogDemo.Entities.Helpers;
using System.Drawing;

namespace LendADogDemo.MVC.Services
{
    public interface IDogService
    {
        bool CreateDog(DogViewModel dogToBeCreated, string userId, HttpPostedFileBase photo);

        bool EditDog(DogViewModel dogToBeEdited);

        bool DeleteDog(int DogId);

        DogViewModel GetDog(int DogID);
    }

    public class DogService : IDogService
    {
        #region Initialize

        private readonly IUnitOfWork unitOfWork;
        private readonly IDogRepository dogRepo;
        private readonly IDogPhotoRepository dogPhotoRepo;
        private readonly IMainMessageBoardRepository mainDashboardRepo;

        public DogService(IUnitOfWork _unitOfWork, IDogRepository _dogRepo, IDogPhotoRepository _dogPhotoRepo,
            IMainMessageBoardRepository _mainDashboardRepo)
        {
            unitOfWork = _unitOfWork;
            dogRepo = _dogRepo;
            dogPhotoRepo = _dogPhotoRepo;
            mainDashboardRepo = _mainDashboardRepo;
        }

        #endregion

        public bool CreateDog(DogViewModel dogToBeCreated, string userId, HttpPostedFileBase uploadPhoto)
        {
            try
            {
                Dog newDog = new Dog()
                {
                    DogOwnerID = userId,
                    DogName = dogToBeCreated.DogName,
                    DogSize = dogToBeCreated.DogSize,
                    Description = dogToBeCreated.Description
                };

                dogRepo.Insert(newDog);
                unitOfWork.Commit();

                DogPhoto newDogPhoto = new DogPhoto()
                {
                    DogID = newDog.DogID,
                    Photo = Image.FromStream(uploadPhoto.InputStream).ResizeImageConvertInBytes(360, 270)
                };

                dogPhotoRepo.Insert(newDogPhoto);
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

        public bool EditDog(DogViewModel dogToBeEdited)
        {
            try
            {
                Dog dog = new Dog()
                {
                    DogID = dogToBeEdited.DogID,
                    DogOwnerID = dogToBeEdited.DogOwnerID,
                    DogName = dogToBeEdited.DogName,
                    DogSize = dogToBeEdited.DogSize,
                    Description = dogToBeEdited.Description
                };
                dogRepo.Update(dog);
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

        public bool DeleteDog(int DogId)
        {
            try
            {
                var mainMessages = mainDashboardRepo.GetMainMessageByDogID(DogId);
                if(mainMessages != null)
                {
                    foreach (var item in mainMessages)
                    {
                        mainDashboardRepo.Delete(item);
                    }
                }
                dogRepo.Delete(DogId);
                unitOfWork.Commit();
                return true;
            }
            catch(EntityException ex)
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

        public DogViewModel GetDog(int DogID)
        {
            var dogy = dogRepo.GetByID(DogID);
            DogViewModel dogForEdit = new DogViewModel()
            {
                DogID = dogy.DogID,
                DogOwnerID = dogy.DogOwnerID,
                DogName = dogy.DogName,
                DogSize = dogy.DogSize,
                Description = dogy.Description
            };
            return dogForEdit;
        }
    }
}
