using System.Collections.Generic;

namespace Cookbook.Client.Module.Core.Data.Models
{
    public class BSRecipe : BSModelBase
    {
        public BSRecipe()
        {
            Ingredients = new List<BSIngredient>();
        }

        public string Name { get; set; }


        public string Description { get; set; }


        public List<BSIngredient> Ingredients { get; set; }
    }
}