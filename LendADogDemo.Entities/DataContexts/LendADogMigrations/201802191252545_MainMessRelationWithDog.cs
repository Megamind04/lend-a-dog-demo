namespace LendADogDemo.Entities.DataContexts.LendADogMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MainMessRelationWithDog : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MainMessageBoard", "DogID", c => c.Int(nullable: false));
            CreateIndex("dbo.MainMessageBoard", "DogID");
            AddForeignKey("dbo.MainMessageBoard", "DogID", "dbo.Dog", "DogID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MainMessageBoard", "DogID", "dbo.Dog");
            DropIndex("dbo.MainMessageBoard", new[] { "DogID" });
            DropColumn("dbo.MainMessageBoard", "DogID");
        }
    }
}
