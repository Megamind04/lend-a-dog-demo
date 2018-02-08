using LendADogDemo.Entities.Models;
using System.Collections.Generic;

namespace LendADogDemo.Entities.Interfaces
{
    public interface IRequestMessageRepository : IRepository<RequestMessage>
    {
        IEnumerable<RequestMessage> GetUnconfirmedRequests(string userID);
    }
}
