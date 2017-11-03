using System;
using System.ComponentModel.DataAnnotations;

namespace CookbookApi.Dto
{
    public class BSIngredientDto
    {
        public int Id { get; set; }
        
        public DateTime Created { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Amount { get; set; }

        public int? RecipeId { get; set; }

    }
}