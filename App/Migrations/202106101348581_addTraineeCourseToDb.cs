namespace App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTraineeCourseToDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TraineeCourses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TraineeId = c.String(nullable: false, maxLength: 128),
                        TraineeName = c.String(),
                        CourseId = c.Int(nullable: false),
                        CourseName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.TraineeId, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.TraineeId)
                .Index(t => t.CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TraineeCourses", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.TraineeCourses", "TraineeId", "dbo.AspNetUsers");
            DropIndex("dbo.TraineeCourses", new[] { "CourseId" });
            DropIndex("dbo.TraineeCourses", new[] { "TraineeId" });
            DropTable("dbo.TraineeCourses");
        }
    }
}
