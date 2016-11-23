namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Animals",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        DOB = c.String(nullable: false),
                        gendder = c.String(),
                        speciesID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Species", t => t.speciesID, cascadeDelete: true)
                .Index(t => t.speciesID);
            
            CreateTable(
                "dbo.Species",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        familyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Families", t => t.familyID, cascadeDelete: true)
                .Index(t => t.familyID);
            
            CreateTable(
                "dbo.Families",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.BehaviourLinks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        speciesID = c.Int(nullable: false),
                        behaviorID = c.Int(nullable: false),
                        behaviour_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Behaviours", t => t.behaviour_ID)
                .ForeignKey("dbo.Species", t => t.speciesID, cascadeDelete: true)
                .Index(t => t.speciesID)
                .Index(t => t.behaviour_ID);
            
            CreateTable(
                "dbo.Behaviours",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BehaviourLinks", "speciesID", "dbo.Species");
            DropForeignKey("dbo.BehaviourLinks", "behaviour_ID", "dbo.Behaviours");
            DropForeignKey("dbo.Animals", "speciesID", "dbo.Species");
            DropForeignKey("dbo.Species", "familyID", "dbo.Families");
            DropIndex("dbo.BehaviourLinks", new[] { "behaviour_ID" });
            DropIndex("dbo.BehaviourLinks", new[] { "speciesID" });
            DropIndex("dbo.Species", new[] { "familyID" });
            DropIndex("dbo.Animals", new[] { "speciesID" });
            DropTable("dbo.Behaviours");
            DropTable("dbo.BehaviourLinks");
            DropTable("dbo.Families");
            DropTable("dbo.Species");
            DropTable("dbo.Animals");
        }
    }
}
