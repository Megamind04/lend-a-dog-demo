using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LendADogDemo.MVC.ViewModels
{
    public class MainDashboardViewModel
    {
        public IEnumerable<DogOwnerLookingForDogSitter> MainMessages { get; set; }
    }

    public class DogOwnerLookingForDogSitter
    {
        public int MainBoardID { get; set; }

        public string RequestSender { get; set; }

        public string RequestMessage { get; set; }


        public string RequestSenderFullName { get; set; }

        public string RequestSenderPhoneNumber { get; set; }


        public int DogID { get; set; }

        [EnumDataType(typeof(Size))]
        public Size? DogSize { get; set; }

        public string DogName { get; set; }

        public string Description { get; set; }

    }
}