namespace flight_planner_data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AirportCode_to_airport : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Airports", "airport", c => c.String());
            DropColumn("dbo.Airports", "AirportCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Airports", "AirportCode", c => c.String());
            DropColumn("dbo.Airports", "airport");
        }
    }
}
