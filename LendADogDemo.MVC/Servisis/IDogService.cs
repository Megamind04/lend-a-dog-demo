using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LendADogDemo.Entities.Interfaces;
using LendADogDemo.Entities.UoW;
using LendADogDemo.MVC.ViewModels;
using LendADogDemo.Entities.Models;
using System.Web;
using System.IO;
using System.Data.Entity.Core;
using System.Diagnostics;
using LendADogDemo.Entities.Helpers;
using System.Drawing;

namespace LendADogDemo.MVC.Servisis
{
    public interface IDogService
    {
        bool CreateDog(DogViewModel dogToBeCreated, string userId, HttpPostedFileBase photo);
        bool DeleteDog(int DogId);
        bool EditDog(DogViewModel dogToBeEdited);
        DogViewModel GetDog(int DogID);
    }

    public class DogService : IDogService
    {
        private IUnitOfWork unitOfWork;

        private IDogRepository dogRepo;
        private IDogPhotoRepository dogPhotoRepo;
        private IMainMessageBoardRepository mainDashboardRepo;

        public DogService(IUnitOfWork _unitOfWork, IDogRepository _dogRepo, IDogPhotoRepository _dogPhotoRepo, IMainMessageBoardRepository _mainDashboardRepo)
        {
            unitOfWork = _unitOfWork;
            dogRepo = _dogRepo;
            dogPhotoRepo = _dogPhotoRepo;
            mainDashboardRepo = _mainDashboardRepo;
        }

        public bool CreateDog(DogViewModel dogToBeCreated, string userId, HttpPostedFileBase uploadPhoto)
        {
            Dog newDog = new Dog()
            {
                DogOwnerID = userId,
                DogName = dogToBeCreated.DogName,
                DogSize = dogToBeCreated.DogSize,
                Description = dogToBeCreated.Description
            };
            try
            {
                dogRepo.Insert(newDog);
                unitOfWork.Commit();

                DogPhoto newDogPhoto = new DogPhoto()
                {
                    DogID = newDog.DogID,
                    //Photo = ConvertPhoto(uploadPhoto)
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

        private static byte[] ConvertPhoto(HttpPostedFileBase photo)
        {
            byte[] newPhoto;
            using (Stream inputStream = photo.InputStream)
            {
                MemoryStream memoryStream = inputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }
                newPhoto = memoryStream.ToArray();
                memoryStream.Close();
            }

            return newPhoto;

        }


    }
}
