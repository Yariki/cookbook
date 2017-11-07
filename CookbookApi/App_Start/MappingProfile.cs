using AutoMapper;
using Cookbook.Data.Models;
using CookbookApi.Dto;

namespace CookbookApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<BSRecipe, BSRecipeDto>()
                .ForMember(r => r.Ingredients, opt => opt.Condition(rp => rp.Ingredients != null))
                .ForMember(r => r.Ingredients, opt => opt.MapFrom(r => r.Ingredients));
            this.CreateMap<BSRecipeDto, BSRecipe>()
                .ForMember(r => r.Ingredients, opt => opt.Condition(rp => rp.Ingredients != null))
                .ForMember(r => r.Ingredients, opt => opt.MapFrom(r => r.Ingredients));


            this.CreateMap<BSIngredient, BSIngredientDto>();
            this.CreateMap<BSIngredientDto, BSIngredient>();
        }
    }
}