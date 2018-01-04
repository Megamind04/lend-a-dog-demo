using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LendADogDemo.Entities.Models
{
    public class RequestMessage
    {
        [Key]
        public int RequestMessID { get; set; }

        [ForeignKey("DogOwner")]
        public string DogOwnerID { get; set; }

        public string RequestFrom { get; set; }

        public string Message { get; set; }



        public virtual DogOwner DogOwner { get; set; }
    }
}
