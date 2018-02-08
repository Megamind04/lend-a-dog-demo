using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LendADogDemo.Entities.Models
{
    public class PrivateMessageBoard :BaseModel
    {
        [Key]
        public int PrivateMessID { get; set; }

        [Required]
        public string SendFromID { get; set; }

        [Required]
        public string RrecivedFromID { get; set; }

        [Required]
        [StringLength(150)]
        public string Message { get; set; } 



        [ForeignKey("SendFromID")]
        public virtual DogOwner SenderOfPrivateMessage { get; set; }

        [ForeignKey("RrecivedFromID")]
        public virtual DogOwner ReceiverOfPrivateMessage { get; set; }
    }
}
