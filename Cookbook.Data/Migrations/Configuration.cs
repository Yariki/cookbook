using Cookbook.Data.Models;

namespace Cookbook.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Cookbook.Data.Context.CookbookDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Cookbook.Data.Context.CookbookDbContext context)
        {
            var recipe = new BSRecipe() {Name = "Recipe 1", Description = "Decription 1", Created = DateTime.Now};
            recipe.Ingredients.Add(new BSIngredient() {Name = "Ingresient 1",Amount = 1,Created = DateTime.Now});
            recipe.Ingredients.Add(new BSIngredient() { Name = "Ingresient 2", Amount = 1.2, Created = DateTime.Now });
            context.Recipes.Add(recipe);

            recipe = new BSRecipe() { Name = "Recipe 2", Description = "Decription 2", Created = DateTime.Now };
            recipe.Ingredients.Add(new BSIngredient() { Name = "Ingresient 3", Amount = 10, Created = DateTime.Now });
            recipe.Ingredients.Add(new BSIngredient() { Name = "Ingresient 4", Amount = 3.44, Created = DateTime.Now });
            context.Recipes.Add(recipe);

            recipe = new BSRecipe() { Name = "Recipe 3", Description = "Decription 3", Created = DateTime.Now };
            recipe.Ingredients.Add(new BSIngredient() { Name = "Ingresient 5", Amount = .67, Created = DateTime.Now });
            recipe.Ingredients.Add(new BSIngredient() { Name = "Ingresient 6", Amount = 5.74, Created = DateTime.Now });
            context.Recipes.Add(recipe);
        }
    }
}
