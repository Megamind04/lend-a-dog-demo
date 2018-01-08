using LendADogDemo.Entities.Interfaces;
using LendADogDemo.Entities.Models;
using LendADogDemo.Entities.DataContexts;

namespace LendADogDemo.Entities.Infrastructure
{
    public class DogPhotoRepository : GenericRepository<DogPhoto>, IDogPhotoRepository
    {
        public DogPhotoRepository(LendADogDemoDb context) : base(context)
        {

        }
    }
}