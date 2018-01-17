using LendADogDemo.Entities.Interfaces;
using LendADogDemo.Entities.Models;
using LendADogDemo.Entities.DataContexts;
using System.Linq;
using System.Data;
using System.Data.Entity;

namespace LendADogDemo.Entities.Infrastructure
{
    public class DogPhotoRepository : GenericRepository<DogPhoto>, IDogPhotoRepository
    {
        public DogPhotoRepository(LendADogDemoDb context) : base(context)
        {

        }
    }
}