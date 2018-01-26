namespace LendADogDemo.Entities.DataContexts.LendADogMigrations
{
    using LendADogDemo.Entities.Helpers;
    using LendADogDemo.Entities.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LendADogDemo.Entities.DataContexts.LendADogDemoDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DataContexts\LendADogMigrations";
        }

        protected override void Seed(LendADogDemo.Entities.DataContexts.LendADogDemoDb context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //var korisnici = context.DogOwners.ToList();
            //foreach (var item in korisnici)
            //{
            //    var doggys = new List<Dog>();
            //    for (int d = 1; d <= 5; d++)
            //    {
            //        string dogyname = RandomGenerator.GetRandomDogName();
            //        doggys.Add(new Dog()
            //        {
            //            DogOwnerID = item.Id,
            //            DogName = dogyname,
            //            DogSize = RandomGenerator.GetRandomDogSize(),
            //            Description = dogyname + RandomGenerator.GetRandomDogDescription()
            //        });
            //    }
            //    doggys.ForEach(x => context.Dogs.AddOrUpdate(c => c.DogID, x));
            //    context.SaveChanges();
            //}

            //var owners = context.DogOwners.ToList();
            //foreach (var owner in owners)
            //{
            //    var dofOfOwner = context.Dogs.Where(us => us.DogOwnerID == owner.Id).ToList();

            //    foreach (var dog in dofOfOwner)
            //    {
            //        context.DogPhotos.AddOrUpdate(c => c.DogPhotoID, new DogPhoto
            //        {
            //            DogID = dog.DogID,
            //            Photo = RandomGenerator.GetRandomDogPhoto()
            //        });
            //        context.SaveChanges();
            //    }
            //}
            if (context.RequestMessages.Count() < 1)
            {
                RandomRequestMessage bla = new RandomRequestMessage(context);
                bla.EditMessages();
            }

        }
    }
}
