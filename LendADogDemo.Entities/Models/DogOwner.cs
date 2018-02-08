using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LendADogDemo.Entities.Models
{
    public class DogOwner
    {
        public string Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [NotMapped]
        public string FullName => FirstName + " " + LastName;

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public bool IsConfirmed { get; set; }

        public string UserName { get; set; }


 
        public virtual ICollection<Dog> Dogs { get; set; }
        public virtual ICollection<RequestMessage> ReceivedRequestMessages { get; set; }
    }
}
