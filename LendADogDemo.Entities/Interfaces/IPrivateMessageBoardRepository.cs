using LendADogDemo.Entities.Models;
using System.Collections.Generic;

namespace LendADogDemo.Entities.Interfaces
{
    public interface IPrivateMessageBoardRepository : IRepository<PrivateMessageBoard>
    {
        IEnumerable<PrivateMessageBoard> GetByDogOwnerId(string userId);

        IEnumerable<PrivateMessageBoard> GetMessagesBetweenTwoUsers(string userId, string otherId);
    }
}
