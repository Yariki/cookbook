namespace Cookbook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init_Migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BSEntityHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        ObjectId = c.Int(nullable: false),
                        Values = c.String(storeType: "xml"),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BSIngredients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Amount = c.Double(nullable: false),
                        RecipeId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BSRecipes", t => t.RecipeId, cascadeDelete: true)
                .Index(t => t.RecipeId);
            
            CreateTable(
                "dbo.BSRecipes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BSIngredients", "RecipeId", "dbo.BSRecipes");
            DropIndex("dbo.BSIngredients", new[] { "RecipeId" });
            DropTable("dbo.BSRecipes");
            DropTable("dbo.BSIngredients");
            DropTable("dbo.BSEntityHistories");
        }
    }
}
