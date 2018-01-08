using LendADogDemo.Entities.Interfaces;
using LendADogDemo.Entities.Models;
using LendADogDemo.Entities.DataContexts;

namespace LendADogDemo.Entities.Infrastructure
{
    public class RequestMessageRepository : GenericRepository<RequestMessage>,IRequestMessageRepository
    {
        public RequestMessageRepository(LendADogDemoDb context) : base(context)
        {

        }
    }
}
