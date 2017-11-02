using System;
using System.Collections.Generic;
using Cookbook.Data.Core;

namespace Cookbook.Data.Models
{
    public class BSRecipe : BSCoreEntity
    {
        public BSRecipe()
        {
            Ingredients = new List<BSIngredient>();
        }

        public string Name { get; set; }


        public string Description { get; set; }


        public virtual ICollection<BSIngredient> Ingredients { get; set; }

    }
}