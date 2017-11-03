using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CookbookApi.Dto
{
    public class BSRecipeDto
    {
        public int Id { get; set; }
        
        public DateTime Created { get; set; }

        [Required]
        public string Name { get; set; }
        
        public string Description { get; set; }


        public virtual ICollection<BSIngredientDto> Ingredients { get; set; }
    }
}