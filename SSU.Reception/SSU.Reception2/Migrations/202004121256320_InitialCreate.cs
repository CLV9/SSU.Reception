using System.Data.Entity.Migrations;

namespace SSU.Reception.Migrations
{
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Enrollees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Surname = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Patronymic = c.String(nullable: false),
                        SchoolId = c.Int(nullable: false),
                        ExamYear = c.Int(nullable: false),
                        PersonalPhone = c.String(nullable: false),
                        AdditionalPhones = c.String(),
                        MathScore = c.Int(nullable: false),
                        RussianScore = c.Int(nullable: false),
                        CSScore = c.Int(),
                        SSScore = c.Int(),
                        HasMedal = c.Boolean(nullable: false),
                        FirstLevelOlympiad = c.Int(nullable: false),
                        OtherOlympiads = c.Int(nullable: false),
                        ReceiptStatus = c.Int(nullable: false),
                        OriginalCertificate = c.Boolean(nullable: false),
                        FirstPriorityId = c.Int(nullable: false),
                        SecondPriorityId = c.Int(),
                        ThirdPriorityId = c.Int(),
                        FirstPriorityNumber = c.Int(nullable: false),
                        SecondPriorityNumber = c.Int(),
                        ThirdPriorityNumber = c.Int(),
                        Comment = c.String(),
                        EnrollmentStatus = c.Boolean(nullable: false),
                        ActivityStatus = c.Boolean(nullable: false),
                        ConversionHistory = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Directions", t => t.FirstPriorityId, cascadeDelete: true)
                .ForeignKey("dbo.Schools", t => t.SchoolId, cascadeDelete: true)
                .ForeignKey("dbo.Directions", t => t.SecondPriorityId)
                .ForeignKey("dbo.Directions", t => t.ThirdPriorityId)
                .Index(t => t.SchoolId)
                .Index(t => t.FirstPriorityId)
                .Index(t => t.SecondPriorityId)
                .Index(t => t.ThirdPriorityId);
            
            CreateTable(
                "dbo.Directions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        BudgetPlaces = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Schools",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Enrollees", "ThirdPriorityId", "dbo.Directions");
            DropForeignKey("dbo.Enrollees", "SecondPriorityId", "dbo.Directions");
            DropForeignKey("dbo.Enrollees", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.Enrollees", "FirstPriorityId", "dbo.Directions");
            DropIndex("dbo.Enrollees", new[] { "ThirdPriorityId" });
            DropIndex("dbo.Enrollees", new[] { "SecondPriorityId" });
            DropIndex("dbo.Enrollees", new[] { "FirstPriorityId" });
            DropIndex("dbo.Enrollees", new[] { "SchoolId" });
            DropTable("dbo.Schools");
            DropTable("dbo.Directions");
            DropTable("dbo.Enrollees");
        }
    }
}
