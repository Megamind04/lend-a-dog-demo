﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LendADogDemo.MVC.ViewModels
{
    public class DogViewModel
    {
        public int DogID { get; set; }

        //[Required]
        public string DogOwnerID { get; set; }

        [Required]
        [EnumDataType(typeof(Size))]
        public Size? DogSize { get; set; }

        [Required]
        [StringLength(50)]
        public string DogName { get; set; }

        [StringLength(150)]
        public string Description { get; set; }

        //public byte[] LastDogPhoto { get; set; }

        //public DogPhotoViewModel Photo { get; set; }
    }

    public class DogPhotoViewModel
    {
        //[Required]
        public int DogID { get; set; }

        //public string DogPhoto { get; set; } 
    }

    //public class DogPhotoUploadViewModel
    //{
    //    public HttpPostedFileBase 
    //}

    public class PrivateMessageBoardViewModel
    {
        [Required]
        public string RrecivedFromID { get; set; }

        [Required]
        [StringLength(150)]
        public string Message { get; set; }
    }
}