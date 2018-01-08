using LendADogDemo.Entities.Interfaces;
using LendADogDemo.Entities.Models;
using LendADogDemo.Entities.DataContexts;

namespace LendADogDemo.Entities.Infrastructure
{
    public class DogRepository : GenericRepository<Dog>, IDogRepository
    {
        public DogRepository(LendADogDemoDb context) : base(context)
        {

        }
    }
}
