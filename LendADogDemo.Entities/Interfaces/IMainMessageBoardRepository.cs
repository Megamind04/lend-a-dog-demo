using LendADogDemo.Entities.Models;
using System.Collections.Generic;

namespace LendADogDemo.Entities.Interfaces
{
    public interface IMainMessageBoardRepository : IRepository<MainMessageBoard>
    {
        IEnumerable<MainMessageBoard> GetUnansweredMessagesWithDogs();
        IEnumerable<MainMessageBoard> GetMainMessageByDogID(int DogID);
    }
}
