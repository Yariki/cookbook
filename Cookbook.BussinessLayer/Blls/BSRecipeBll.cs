using System.Linq;
using Cookbook.BussinessLayer.Core;
using Cookbook.BussinessLayer.Interfaces;
using Cookbook.Data.Core;
using Cookbook.Data.Interfaces;
using Cookbook.Data.Models;
using Ninject;

namespace Cookbook.BussinessLayer.Blls
{
    public class BSRecipeBll : BSBussinessEntityBll<BSRecipe> , IBSRecipeBll
    {
        public BSRecipeBll()
        {
        }

        public BSRecipeBll(IBSContextFactory contextFactory) : base(contextFactory)
        {
        }

        [Inject]
        public IBSIngredientBll IngredientBll { get; set; }

        public override void Update(BSRecipe entity)
        {
            if (entity.Ingredients != null && entity.Ingredients.Any())
            {
                UpdateIngredients(entity);
            }
            base.Update(entity);
        }

        private void UpdateIngredients(BSRecipe entity)
        {
            var original = GetById(entity.Id);

            var ingAdded = entity.Ingredients.ExceptBy(original.Ingredients, i => i.Id).ToList();
            var ingDeleted = original.Ingredients.ExceptBy(entity.Ingredients, i => i.Id).ToList();
            var ingUpdated = entity.Ingredients.Select(i => i.Id).Intersect(original.Ingredients.Select(i => i.Id)).ToList();

            if (ingAdded.Any())
            {
                ingAdded.ForEach(i => IngredientBll.Insert(i));
            }
            if (ingDeleted.Any())
            {
                ingDeleted.ForEach(i => IngredientBll.Delete(i));
            }
            if (ingUpdated.Any())
            {
                ingUpdated.ForEach(i =>
                {
                    var mapped = entity.Ingredients.SingleOrDefault(ing => ing.Id == i);
                    if (mapped != null)
                    {
                        IngredientBll.Update(mapped);
                    }
                });
            }
        }
    }
}