using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Cookbook.Data.Core;

namespace Cookbook.Data.Models
{
    public class BSIngredient : BSCoreEntity
    {
        public string Name { get; set; }

        public double Amount { get; set; }

        public int? RecipeId { get; set; }
        
        [XmlIgnore]
        [IgnoreDataMember]
        public BSRecipe Recipe { get; set; }
    }
}