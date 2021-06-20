namespace App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TraineeUsers", "FullName", c => c.String(nullable: false));
            AlterColumn("dbo.TraineeUsers", "Telephone", c => c.String(nullable: false));
            AlterColumn("dbo.TrainerUsers", "FullName", c => c.String(nullable: false));
            AlterColumn("dbo.TrainerUsers", "Telephone", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TrainerUsers", "Telephone", c => c.String());
            AlterColumn("dbo.TrainerUsers", "FullName", c => c.String());
            AlterColumn("dbo.TraineeUsers", "Telephone", c => c.String());
            AlterColumn("dbo.TraineeUsers", "FullName", c => c.String());
        }
    }
}
