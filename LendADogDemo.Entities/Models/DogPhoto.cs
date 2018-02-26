using System.ComponentModel.DataAnnotations;

namespace LendADogDemo.Entities.Models
{
    public class DogPhoto
    {
        [Key]
        public int DogPhotoID { get; set; }

        public int DogID { get; set; }

        public byte[] Photo { get; set; }
    }
}
