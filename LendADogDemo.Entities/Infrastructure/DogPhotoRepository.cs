using LendADogDemo.Entities.Interfaces;
using LendADogDemo.Entities.Models;
using LendADogDemo.Entities.UoW;

namespace LendADogDemo.Entities.Infrastructure
{
    public class DogPhotoRepository : GenericRepository<DogPhoto>, IDogPhotoRepository
    {
        public DogPhotoRepository(IUnitOfWork _unitOfWork) : base(_unitOfWork)
        {

        }
    }
}