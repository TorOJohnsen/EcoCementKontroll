namespace CementControl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Reworkmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CementData", "CurrentWeight", c => c.Double(nullable: false));
            AddColumn("dbo.CementData", "CurrentVoltage", c => c.Double(nullable: false));
            AddColumn("dbo.CementData", "CementLoadGoal", c => c.Double(nullable: false));
            AddColumn("dbo.CementData", "CementLoaded", c => c.Double(nullable: false));
            AddColumn("dbo.CementData", "StartingWeight", c => c.Double(nullable: false));
            AlterColumn("dbo.CementData", "State", c => c.String());
            DropColumn("dbo.CementData", "Weight");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CementData", "Weight", c => c.Double(nullable: false));
            AlterColumn("dbo.CementData", "State", c => c.Int(nullable: false));
            DropColumn("dbo.CementData", "StartingWeight");
            DropColumn("dbo.CementData", "CementLoaded");
            DropColumn("dbo.CementData", "CementLoadGoal");
            DropColumn("dbo.CementData", "CurrentVoltage");
            DropColumn("dbo.CementData", "CurrentWeight");
        }
    }
}
