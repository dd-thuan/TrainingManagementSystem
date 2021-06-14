namespace App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteAge : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TraineeUsers", "age");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TraineeUsers", "age", c => c.Int(nullable: false));
        }
    }
}
