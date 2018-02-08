using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

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
