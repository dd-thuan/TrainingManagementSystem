namespace App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateCourseToCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Courses", "CategoryId");
            AddForeignKey("dbo.Courses", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Courses", new[] { "CategoryId" });
            DropColumn("dbo.Courses", "CategoryId");
        }
    }
}
