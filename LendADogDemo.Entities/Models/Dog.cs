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

        [Required]
        [ForeignKey("DogOwner")]
        public string DogOwnerID { get; set; }

        [Required]
        [EnumDataType(typeof(Size))]
        public Size? DogSize { get; set; }

        [Required]
        [StringLength(50)]
        public string DogName { get; set; }

        [StringLength(150)]
        public string Description { get; set; }

        

        public virtual ICollection<DogPhoto> DogPhotos { get; set; }
        public virtual DogOwner DogOwner { get; set; }
    }
}
