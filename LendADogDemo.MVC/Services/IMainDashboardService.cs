using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LendADogDemo.Entities.Interfaces;
using LendADogDemo.Entities.UoW;
using LendADogDemo.MVC.ViewModels;

namespace LendADogDemo.MVC.Services
{
    public interface IMainDashboardService
    {
        MainDashboardViewModel GetMainDashboardModel();
    }

    public class MainDashboardService : IMainDashboardService
    {
        #region Initialize

        private readonly IUnitOfWork unitOfWork;
        private readonly IMainMessageBoardRepository mainMessageBoardRepo;

        public MainDashboardService(IUnitOfWork _unitOfWork, IMainMessageBoardRepository _mainMessageBoardRepo)
        {
            unitOfWork = _unitOfWork;
            mainMessageBoardRepo = _mainMessageBoardRepo;
        }

        #endregion

        public MainDashboardViewModel GetMainDashboardModel()
        {
            IEnumerable<DogOwnerLookingForDogSitter> GetMainMessages()
            {
                return mainMessageBoardRepo.GetUnansweredMessagesWithDogs().Select(m => new DogOwnerLookingForDogSitter()
                {
                    MainBoardID = m.MainBoardID,
                    RequestMessage = m.RequestMessage,

                    RequestSender = m.DogOwnerID,
                    RequestSenderFullName = m.RequestSender.FullName,
                    RequestSenderPhoneNumber = m.RequestSender.PhoneNumber,

                    DogID = m.DogID,
                    DogName = m.Dog.DogName,
                    DogSize = m.Dog.DogSize,
                    Description = m.Dog.Description,  
                });
            }

            MainDashboardViewModel mainDashboardViewModel = new MainDashboardViewModel()
            {
                MainMessages = GetMainMessages()
            };

            return mainDashboardViewModel;

        }
    }
}
