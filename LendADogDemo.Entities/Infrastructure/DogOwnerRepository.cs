using LendADogDemo.Entities.Interfaces;
using LendADogDemo.Entities.Models;
using LendADogDemo.Entities.UoW;

namespace LendADogDemo.Entities.Infrastructure
{
    public class DogOwnerRepository : GenericRepository<DogOwner> ,IDogOwnerRepository
    {
        public DogOwnerRepository(IUnitOfWork _unitOfWork) : base(_unitOfWork)
        {

        }
    }
}
