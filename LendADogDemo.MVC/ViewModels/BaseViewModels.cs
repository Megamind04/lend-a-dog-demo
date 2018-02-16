using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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

        [StringLength(150)]
        public string Description { get; set; }

        public string LastDogPhoto { get; set; }
    }

    public class DogPhotoViewModel
    {
    }

    public class PrivateMessageBoardViewModel
    {
        [Required]
        public string RrecivedFromID { get; set; }

        [Required]
        [StringLength(150)]
        public string Message { get; set; }
    }
}