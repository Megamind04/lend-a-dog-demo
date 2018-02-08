using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LendADogDemo.MVC.ViewModels
{
    public class PersonalDashboardViewModel
    {
        public IEnumerable<NotConfirmedUsersRequestViewModel> NotConfirmedUsersRequests { get; set; }

        public IEnumerable<ConversationViewModel> Conversations { get; set; }

        public IEnumerable<DogViewModel> Dogs { get; set; }
    }

    public class NotConfirmedUsersRequestViewModel
    {
        public string RequestFromID { get; set; }

        public string RequestFromFullName { get; set; }

        public string Message { get; set; }
    }

    public class ConversationViewModel
    {
        public string OtherID { get; set; }

        public string OtherFullName { get; set; }

        public string LastMessage { get; set; }
    }

}