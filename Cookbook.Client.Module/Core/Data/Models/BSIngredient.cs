namespace Cookbook.Client.Module.Core.Data.Models
{
    public class BSIngredient : BSModelBase
    {
        public BSIngredient()
        {
            
        }

        public string Name { get; set; }

        public double Amount { get; set; }

        public int? RecipeId { get; set; }

        public BSRecipe Recipe { get; set; }
    }
}