namespace LendADogDemo.Entities.DataContexts.LendADogMigrations
{
    using System;
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
        }
    }
}
