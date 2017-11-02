using System.Data.Entity;
using Cookbook.Data.Interfaces;
using Cookbook.Data.Models;

namespace Cookbook.Data.Context
{
    public class CookbookDbContext : DbContext, IBSDataContext
    {

        public DbSet<BSRecipe> Recipes { get; set; }

        public DbSet<BSIngredient> Ingredients { get; set; }
        
        public DbSet<BSEntityHistory> Histories { get; set; }
        
        public CookbookDbContext()
            :base("Name=CookbookConnection")
        {   
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BSRecipe>()
                .HasMany(r => r.Ingredients)
                .WithRequired(i => i.Recipe)
                .WillCascadeOnDelete();
            base.OnModelCreating(modelBuilder);
        }
    }
}