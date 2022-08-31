namespace CountingEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CounterItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ThreadCount = c.Int(nullable: false),
                        LastCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 50),
                        RequestDate = c.DateTime(nullable: false),
                        RequestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CounterItems", t => t.RequestId)
                .Index(t => t.RequestId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sessions", "RequestId", "dbo.CounterItems");
            DropIndex("dbo.Sessions", new[] { "RequestId" });
            DropTable("dbo.Sessions");
            DropTable("dbo.CounterItems");
        }
    }
}
