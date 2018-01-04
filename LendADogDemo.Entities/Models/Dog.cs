using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

public enum Size
{
    Small,
    Average,
    Large
}

namespace LendADogDemo.Entities.Models
{
    public class Dog
    {
        [Key]
        public int DogID { get; set; }

        [ForeignKey("DogOwner")]
        public string DogOwnerID { get; set; }

        [EnumDataType(typeof(Size))]
        public Size? DogSize { get; set; }

        public string DogName { get; set; }

        public string Description { get; set; }

        

        public virtual ICollection<DogPhoto> DogPhotos { get; set; }
        public virtual DogOwner DogOwner { get; set; }
    }
}
