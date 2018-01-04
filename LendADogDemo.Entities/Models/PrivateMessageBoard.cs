using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LendADogDemo.Entities.Models
{
    public class PrivateMessageBoard
    {
        [Key]
        public int PrivateMessID { get; set; }

        public string SenderID { get; set; }

        public string ReceiverID { get; set; }

        public string Message { get; set; } 



        [ForeignKey("SenderID")]
        [InverseProperty("SenderDogOwners")]
        public virtual DogOwner SenderDogOwner { get; set; }

        [ForeignKey("ReceiverID")]
        [InverseProperty("ReceiverDogOwners")]
        public virtual DogOwner ReceiverDogOwner { get; set; }
    }
}
