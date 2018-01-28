using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LendADogDemo.Entities.Models
{
    public class RequestMessage :BaseModel
    {
        [Key]
        public int RequestMessID { get; set; }

        [Required]
        public string SendFromID { get; set; }

        [Required]
        public string ReceiverID { get; set; }

        public string Message { get; set; }



        [ForeignKey("SendFromID")]
        [InverseProperty("SendRequestMessages")]
        public virtual DogOwner SendRequestMessage { get; set; }

        [ForeignKey("ReceiverID")]
        [InverseProperty("ReceivedRequestMessages")]
        public virtual DogOwner ReceivedRequestMessage { get; set; }
    }
}
