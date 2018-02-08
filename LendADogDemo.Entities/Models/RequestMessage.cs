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
        public string ReciverID { get; set; }

        public string Message { get; set; }


        [ForeignKey("SendFromID")]
        public virtual DogOwner SenderOfRequest { get; set; }

    }
}
