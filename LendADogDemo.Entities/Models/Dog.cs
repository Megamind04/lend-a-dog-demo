using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


public enum Size
{
    Small = 1,
    Average = 2,
    Large = 3
}

namespace LendADogDemo.Entities.Models
{
    public class Dog
    {
        [Key]
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


        
        public virtual ICollection<DogPhoto> DogPhotos { get; set; }
    }
}
