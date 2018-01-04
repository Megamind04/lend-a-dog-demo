using System.Collections.Generic;

namespace LendADogDemo.Entities.Models
{
    public class DogOwner
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsConfirmed { get; set; }

        public string UserName { get; set; }



        public virtual ICollection<Dog> Dogs { get; set; }
        public virtual ICollection<PrivateMessageBoard> SenderDogOwners { get; set; }
        public virtual ICollection<PrivateMessageBoard> ReceiverDogOwners { get; set; }
        public virtual ICollection<RequestMessage> RequestMessages { get; set; }
    }
}
