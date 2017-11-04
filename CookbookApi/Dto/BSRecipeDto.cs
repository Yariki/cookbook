using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CookbookApi.Dto
{
    public class BSRecipeDto
    {
        public BSRecipeDto()
        {
            Ingredients = new List<BSIngredientDto>();
        }

        public int Id { get; set; }
        
        public DateTime Created { get; set; }

        [Required]
        public string Name { get; set; }
        
        public string Description { get; set; }


        public ICollection<BSIngredientDto> Ingredients { get; set; }
    }
}