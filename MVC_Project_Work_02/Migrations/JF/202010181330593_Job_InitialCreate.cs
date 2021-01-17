namespace MVC_Project_Work_02.Migrations.JF
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Job_InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobAdvertisements",
                c => new
                    {
                        JobAdvertisementId = c.Int(nullable: false, identity: true),
                        JobAdvertisementName = c.String(nullable: false, maxLength: 50),
                        Post = c.Int(nullable: false),
                        OnlineJobSiteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.JobAdvertisementId)
                .ForeignKey("dbo.OnlineJobSites", t => t.OnlineJobSiteId, cascadeDelete: true)
                .Index(t => t.OnlineJobSiteId);
            
            CreateTable(
                "dbo.OnlineJobSites",
                c => new
                    {
                        OnlineJobSiteId = c.Int(nullable: false, identity: true),
                        OnlineJobSiteName = c.String(nullable: false, maxLength: 50),
                        StartedJourney = c.DateTime(nullable: false, storeType: "date"),
                        Web = c.String(),
                        Response = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.OnlineJobSiteId);
            
            CreateTable(
                "dbo.RegisterApplicants",
                c => new
                    {
                        RegisterApplicantId = c.Int(nullable: false, identity: true),
                        RegisterApplicantName = c.String(nullable: false, maxLength: 50),
                        ContactEmail = c.String(nullable: false, maxLength: 50),
                        OnlineJobSiteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RegisterApplicantId)
                .ForeignKey("dbo.OnlineJobSites", t => t.OnlineJobSiteId, cascadeDelete: true)
                .Index(t => t.OnlineJobSiteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobAdvertisements", "OnlineJobSiteId", "dbo.OnlineJobSites");
            DropForeignKey("dbo.RegisterApplicants", "OnlineJobSiteId", "dbo.OnlineJobSites");
            DropIndex("dbo.RegisterApplicants", new[] { "OnlineJobSiteId" });
            DropIndex("dbo.JobAdvertisements", new[] { "OnlineJobSiteId" });
            DropTable("dbo.RegisterApplicants");
            DropTable("dbo.OnlineJobSites");
            DropTable("dbo.JobAdvertisements");
        }
    }
}
