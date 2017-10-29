namespace UniSocial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogPosts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Link = c.String(),
                        Title = c.String(),
                        Excerpt = c.String(),
                        Date = c.DateTime(nullable: false),
                        FeaturedImage = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BlogWebsites",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Url = c.String(),
                        SchoolID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Schools", t => t.SchoolID, cascadeDelete: true)
                .Index(t => t.SchoolID);
            
            CreateTable(
                "dbo.Schools",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        SchoolName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BlogWebsites", "SchoolID", "dbo.Schools");
            DropIndex("dbo.BlogWebsites", new[] { "SchoolID" });
            DropTable("dbo.Schools");
            DropTable("dbo.BlogWebsites");
            DropTable("dbo.BlogPosts");
        }
    }
}
