using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LendADogDemo.Entities.Models
{
    public class MainMessageBoard
    {
        [Key]
        public int MainBoardID { get; set; }

        [ForeignKey("DogOwner")]
        public string DogOwnerID { get; set; }

        public string RequestMessage { get; set; }

        public DateTime MessageCreated { get; set; }

        public bool Answered { get; set; }



        public virtual DogOwner DogOwner { get; set; }
    }
}
