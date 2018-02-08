using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LendADogDemo.Entities.Models
{
    public class MainMessageBoard : BaseModel
    {
        [Key]
        public int MainBoardID { get; set; }

        [Required]
        public int DogID { get; set; }

        [Required]
        [ForeignKey("RequestSender")]
        public string DogOwnerID { get; set; }

        [Required]
        [StringLength(150)]
        public string RequestMessage { get; set; }


        public bool Answered { get; set; }


        public virtual Dog Dog { get; set; }

        public virtual DogOwner RequestSender { get; set; }
    }
}
