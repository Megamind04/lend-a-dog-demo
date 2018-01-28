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

            //if (System.Diagnostics.Debugger.IsAttached == false)
            //{
            //    System.Diagnostics.Debugger.Launch();
            //}

            if (!context.Dogs.Any())
            {
                context.RandomDogsPerDogOwner(3, 3);
            }

            if (!context.RequestMessages.Any())
            {
                context.RandomRequestMessages(3);
            }

            if (!context.MainMessages.Any())
            {
                context.RandomMainBoardMessages(2);
            }

            if (!context.PrivateMessages.Any())
            {
                context.RandomPrivateBoardMessages(5, 3);
            }

        }
    }
}
