namespace DatabaseContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bug",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 250),
                        Details = c.String(),
                        IsOpen = c.Boolean(nullable: false),
                        OpenedDate = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Title, unique: true);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserBug",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Bug_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Bug_Id })
                .ForeignKey("dbo.User", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Bug", t => t.Bug_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Bug_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserBug", "Bug_Id", "dbo.Bug");
            DropForeignKey("dbo.UserBug", "User_Id", "dbo.User");
            DropIndex("dbo.UserBug", new[] { "Bug_Id" });
            DropIndex("dbo.UserBug", new[] { "User_Id" });
            DropIndex("dbo.Bug", new[] { "Title" });
            DropTable("dbo.UserBug");
            DropTable("dbo.User");
            DropTable("dbo.Bug");
        }
    }
}
