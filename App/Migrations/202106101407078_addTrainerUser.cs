namespace App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTrainerUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TrainerUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        FullName = c.String(),
                        type = c.Int(nullable: false),
                        Telephone = c.String(),
                        WorkingPlace = c.String(),
                        EmailAddress = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrainerUsers", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.TrainerUsers", new[] { "Id" });
            DropTable("dbo.TrainerUsers");
        }
    }
}
