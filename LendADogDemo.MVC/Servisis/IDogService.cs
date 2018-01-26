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

namespace LendADogDemo.MVC.Servisis
{
    public interface IDogService
    {
        bool CreateDog(DogViewModel dogToBeCreated, string userId, HttpPostedFileBase photo);
    }




    public class DogService : IDogService
    {
        private IUnitOfWork _unitOfWork;

        private IDogRepository _dogRepo;
        private IDogPhotoRepository _dogPhotoRepo;

        public DogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _dogRepo = _unitOfWork.DogRepo;
            _dogPhotoRepo = _unitOfWork.DogPhotoRepo;
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
                _dogRepo.Insert(newDog);
                _unitOfWork.Save();

                DogPhoto newDogPhoto = new DogPhoto()
                {
                    DogID = newDog.DogID,
                    Photo = ConvertPhoto(uploadPhoto)
                };

                _dogPhotoRepo.Insert(newDogPhoto);
                _unitOfWork.Save();

                return true;
            }
            catch (EntityException ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
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
