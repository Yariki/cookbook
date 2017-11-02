using System;
using Cookbook.Data.Core;

namespace Cookbook.Data.Models
{
    public class BSIngredient : BSCoreEntity
    {
        public string Name { get; set; }

        public double Amount { get; set; }

        public int? RecipeId { get; set; }

        public BSRecipe Recipe { get; set; }
    }
}