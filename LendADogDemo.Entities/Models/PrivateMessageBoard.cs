using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LendADogDemo.Entities.Models
{
    public class PrivateMessageBoard :BaseModel
    {
        [Key]
        public int PrivateMessID { get; set; }

        [Required]
        public string SenderID { get; set; }

        [Required]
        public string ReceiverID { get; set; }

        [Required]
        [StringLength(150)]
        public string Message { get; set; } 



        [ForeignKey("SenderID")]
        [InverseProperty("SenderDogOwners")]
        public virtual DogOwner SenderDogOwner { get; set; }

        [ForeignKey("ReceiverID")]
        [InverseProperty("ReceiverDogOwners")]
        public virtual DogOwner ReceiverDogOwner { get; set; }
    }
}
