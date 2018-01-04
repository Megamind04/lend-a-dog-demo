namespace LendADogDemo.Entities.DataContexts.LendADogMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.AspNetUsers",
            //    c => new
            //        {
            //            Id = c.String(nullable: false, maxLength: 128),
            //            FirstName = c.String(),
            //            LastName = c.String(),
            //            Email = c.String(),
            //            PhoneNumber = c.String(),
            //            IsConfirmed = c.Boolean(nullable: false),
            //            UserName = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Dog",
                c => new
                    {
                        DogID = c.Int(nullable: false, identity: true),
                        DogOwnerID = c.String(nullable:false,maxLength: 128),
                        DogSize = c.Int(nullable:false),
                        DogName = c.String(nullable:false,maxLength:50),
                        Description = c.String(maxLength:150),
                    })
                .PrimaryKey(t => t.DogID)
                //.ForeignKey("dbo.AspNetUsers", t => t.DogOwnerID)
                .Index(t => t.DogOwnerID);
            AddForeignKey("dbo.Dog", "DogOwnerID", "dbo.AspNetUsers", "Id",cascadeDelete:true);

            CreateTable(
                "dbo.DogPhoto",
                c => new
                    {
                        DogPhotoID = c.Int(nullable: false, identity: true),
                        DogID = c.Int(nullable: false),
                        Photo = c.Binary(),
                    })
                .PrimaryKey(t => t.DogPhotoID)
                .ForeignKey("dbo.Dog", t => t.DogID, cascadeDelete: true)
                .Index(t => t.DogID);
            
            CreateTable(
                "dbo.PrivateMessageBoard",
                c => new
                    {
                        PrivateMessID = c.Int(nullable: false, identity: true),
                        SenderID = c.String(nullable:false,maxLength: 128),
                        ReceiverID = c.String(nullable: false,maxLength: 128),
                        Message = c.String(nullable: false,maxLength:150),
                    })
                .PrimaryKey(t => t.PrivateMessID)
                //.ForeignKey("dbo.AspNetUsers", t => t.ReceiverID)
                //.ForeignKey("dbo.AspNetUsers", t => t.SenderID)
                .Index(t => t.SenderID)
                .Index(t => t.ReceiverID);
            AddForeignKey("dbo.PrivateMessageBoard", "ReceiverID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PrivateMessageBoard", "SenderID", "dbo.AspNetUsers", "Id");

            CreateTable(
                "dbo.RequestMessage",
                c => new
                {
                    RequestMessID = c.Int(nullable: false, identity: true),
                    DogOwnerID = c.String(nullable: false, maxLength: 128),
                    RequestFrom = c.String(nullable: false, maxLength: 128),
                    Message = c.String(),
                })
                .PrimaryKey(t => t.RequestMessID)
                //.ForeignKey("dbo.AspNetUsers", t => t.DogOwnerID)
                .Index(t => t.DogOwnerID)
                .Index(t => t.RequestFrom, unique: true);
            AddForeignKey("dbo.RequestMessage", "DogOwnerID", "dbo.AspNetUsers", "Id", cascadeDelete: true);

            CreateTable(
                "dbo.MainMessageBoard",
                c => new
                    {
                        MainBoardID = c.Int(nullable: false, identity: true),
                        DogOwnerID = c.String(nullable:false,maxLength: 128),
                        RequestMessage = c.String(nullable:false,maxLength:150),
                        MessageCreated = c.DateTime(nullable: false,defaultValueSql: "GETDATE()"),
                        Answered = c.Boolean(nullable: false,defaultValue:false),
                    })
                .PrimaryKey(t => t.MainBoardID)
                //.ForeignKey("dbo.AspNetUsers", t => t.DogOwnerID)
                .Index(t => t.DogOwnerID);
            AddForeignKey("dbo.MainMessageBoard", "DogOwnerID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MainMessageBoard", "DogOwnerID", "dbo.AspNetUsers");
            DropForeignKey("dbo.RequestMessage", "DogOwnerID", "dbo.AspNetUsers");
            DropForeignKey("dbo.PrivateMessageBoard", "SenderID", "dbo.AspNetUsers");
            DropForeignKey("dbo.PrivateMessageBoard", "ReceiverID", "dbo.AspNetUsers");
            DropForeignKey("dbo.DogPhoto", "DogID", "dbo.Dog");
            DropForeignKey("dbo.Dog", "DogOwnerID", "dbo.AspNetUsers");
            DropIndex("dbo.MainMessageBoard", new[] { "DogOwnerID" });
            DropIndex("dbo.RequestMessage", new[] { "DogOwnerID" });
            DropIndex("dbo.PrivateMessageBoard", new[] { "ReceiverID" });
            DropIndex("dbo.PrivateMessageBoard", new[] { "SenderID" });
            DropIndex("dbo.DogPhoto", new[] { "DogID" });
            DropIndex("dbo.Dog", new[] { "DogOwnerID" });
            DropTable("dbo.MainMessageBoard");
            DropTable("dbo.RequestMessage");
            DropTable("dbo.PrivateMessageBoard");
            DropTable("dbo.DogPhoto");
            DropTable("dbo.Dog");
            DropTable("dbo.AspNetUsers");
        }
    }
}
