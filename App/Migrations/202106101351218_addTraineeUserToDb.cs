namespace App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTraineeUserToDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TraineeUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        FullName = c.String(),
                        age = c.Int(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        Telephone = c.String(),
                        mainProgrammingLanguage = c.String(),
                        ToeicScore = c.String(),
                        Department = c.String(),
                        EmailAddress = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TraineeUsers", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.TraineeUsers", new[] { "Id" });
            DropTable("dbo.TraineeUsers");
        }
    }
}
