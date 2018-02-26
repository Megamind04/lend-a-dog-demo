using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LendADogDemo.MVC.ViewModels
{
    public class DogViewModel
    {
        public int DogID { get; set; }

        public string DogOwnerID { get; set; }

        [Required]
        [EnumDataType(typeof(Size))]
        public Size? DogSize { get; set; }

        [Required]
        [StringLength(50)]
        public string DogName { get; set; }

        [Required]
        [StringLength(150)]
        public string Description { get; set; }

        public string LastDogPhoto { get; set; }
    }

    public class PrivateMessageBoardViewModel
    {
        [Required]
        public string RrecivedFromID { get; set; }

        [Required]
        [StringLength(150)]
        public string Message { get; set; }
    }

    public class NotConfirmedUsersRequestViewModel
    {
        [Required]
        public string RequestFromID { get; set; }

        public string RequestFromFullName { get; set; }

        public string Message { get; set; }
    }

    public class ConversationViewModel
    {
        public string OtherID { get; set; }

        public string OtherFullName { get; set; }

        [Required]
        [StringLength(150)]
        public string LastMessage { get; set; }

        
        [DataType(DataType.DateTime)]
        public DateTime? DateTime { get; set; }
    }

    public class PrivateConversationBetweenTwoUsers
    {
        public IEnumerable<ConversationViewModel> Conversations { get; set; }

        public ConversationViewModel NewConversation { get; set; }
    }
}