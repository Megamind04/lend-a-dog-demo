using LendADogDemo.Entities.Interfaces;
using LendADogDemo.Entities.Models;
using LendADogDemo.Entities.DataContexts;

namespace LendADogDemo.Entities.Infrastructure
{
    public class DogOwnerRepository : GenericRepository<DogOwner> ,IDogOwnerRepository
    {
        public DogOwnerRepository(LendADogDemoDb context) : base(context)
        {
                
        }
    }
}
