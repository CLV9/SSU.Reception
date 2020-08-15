using System.Data.Entity.Migrations;

namespace SSU.Reception.Migrations
{
    public partial class DirectionPlaces : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Directions", "PrivilegedPlaces", c => c.Int(nullable: false));
            AddColumn("dbo.Directions", "TargetPlaces", c => c.Int(nullable: false));
            AddColumn("dbo.Directions", "WithoutExamsPlaces", c => c.Int(nullable: false));
            AddColumn("dbo.Directions", "FirstWavePlaces", c => c.Int(nullable: false));
            AddColumn("dbo.Directions", "SecondWavePlaces", c => c.Int(nullable: false));
            DropColumn("dbo.Directions", "PrioritySubject");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Directions", "PrioritySubject", c => c.Int(nullable: false));
            DropColumn("dbo.Directions", "SecondWavePlaces");
            DropColumn("dbo.Directions", "FirstWavePlaces");
            DropColumn("dbo.Directions", "WithoutExamsPlaces");
            DropColumn("dbo.Directions", "TargetPlaces");
            DropColumn("dbo.Directions", "PrivilegedPlaces");
        }
    }
}
