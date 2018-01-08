using LendADogDemo.Entities.Interfaces;
using LendADogDemo.Entities.Models;
using LendADogDemo.Entities.DataContexts;

namespace LendADogDemo.Entities.Infrastructure
{
    public class PrivateMessageBoardRepository : GenericRepository<PrivateMessageBoard>,IPrivateMessageBoardRepository
    {
        public PrivateMessageBoardRepository(LendADogDemoDb context) : base(context)
        {

        }
    }
}
