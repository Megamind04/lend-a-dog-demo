namespace LendADogDemo.Entities.DataContexts.IdentityMigrations
{
    using LendADogDemo.Entities.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using LendADogDemo.Entities.Helpers;

    internal sealed class Configuration : DbMigrationsConfiguration<LendADogDemo.Entities.DataContexts.IdentityDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DataContexts\IdentityMigrations";
        }

        protected override void Seed(LendADogDemo.Entities.DataContexts.IdentityDb context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                        
                var user = new ApplicationUser();
                user.FirstName = "Vlatko";
                user.LastName = "Karceski";
                user.PhoneNumber = "070272405";
                user.UserName = "karcomkc@gmail.com";
                user.Email = "karcomkc@gmail.com";
                user.IsConfirmed = true;

                string userPWD = "Maloto*050582";
                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
            }

            // creating Creating Manager role
            if (!roleManager.RoleExists("DogOwner"))
            {

                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "DogOwner";
                roleManager.Create(role);
            }

            //for (int i = 1; i <= 30; i++)
            //{
            //    string userEmail = RandomGenerator.GetRandomEmail(5);
            //    string password = "Password*1";
            //    if (!context.Users.Any(u => u.UserName == userEmail))
            //    {
            //        var store = new UserStore<ApplicationUser>(context);
            //        var menager = new UserManager<ApplicationUser>(store);
            //        var user = new ApplicationUser()
            //        {
            //            FirstName = RandomGenerator.GetRandomFirstOrLastName(FirstOrLastName.firstName),
            //            LastName = RandomGenerator.GetRandomFirstOrLastName(FirstOrLastName.lastName),
            //            PhoneNumber = RandomGenerator.GetRandomPhoneNumer(9),
            //            UserName = userEmail,
            //            Email = userEmail,
            //            IsConfirmed = false,
            //            LockoutEnabled = true
            //        };
            //        menager.Create(user, password);
            //    }
            //}
        }
    }
}
