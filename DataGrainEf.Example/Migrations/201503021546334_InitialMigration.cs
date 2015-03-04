namespace DataGrainEf.Example.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.__DataMigration",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MigrationId = c.String(),
                        ContextKey = c.String(),
                        MigrationName = c.String(),
                        AppliedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedOn = c.DateTime(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "AccountId", "dbo.Accounts");
            DropIndex("dbo.Orders", new[] { "AccountId" });
            DropTable("dbo.Orders");
            DropTable("dbo.Accounts");
            DropTable("dbo.__DataMigration");
        }
    }
}
