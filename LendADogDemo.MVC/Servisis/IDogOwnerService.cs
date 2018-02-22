using LendADogDemo.Entities.Interfaces;
using LendADogDemo.Entities.UoW;
using LendADogDemo.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LendADogDemo.MVC.Servisis
{
    public interface IDogOwnerService
    {
        NotConfirmedUsersRequestViewModel GetUnverifiedDogOwner(string UserID);
    }

    public class DogOwnerService : IDogOwnerService
    {
        #region Initialize

        private readonly IUnitOfWork unitOfWork;
        private readonly IDogOwnerRepository dogOwnerRepo;

        public DogOwnerService(IUnitOfWork _unitOfWork, IDogOwnerRepository _dogOwnerRepo)
        {
            unitOfWork = _unitOfWork;
            dogOwnerRepo = _dogOwnerRepo;
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
    }
}
