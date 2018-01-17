﻿using LendADogDemo.Entities.Models;


namespace LendADogDemo.Entities.Interfaces
{
    public interface IDogOwnerRepository : IRepository<DogOwner>
    {
        DogOwner GetByEmail(string email);
    }
}
