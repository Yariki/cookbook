using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Cookbook.BussinessLayer.Interfaces;
using Cookbook.Data.Models;
using CookbookApi.Dto;
using CookbookApi.Interfaces;

namespace CookbookApi.Controllers
{
    public class RecipeController : ApiController
    {
        private IBSRecipeBll recipeBll;
        private IBSEntityHistoryBll historyBll;
        private IBSLogger logger;
        
        public RecipeController(IBSRecipeBll recipeBll, IBSEntityHistoryBll historyBll, IBSLogger logger)
        {
            this.recipeBll = recipeBll;
            this.historyBll = historyBll;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllRecipes()
        {
            try
            {
                var recipes = await recipeBll.GetAllAsync();
                return Ok(recipes.Select(Mapper.Map<BSRecipe, BSRecipeDto>));
            }
            catch (Exception e)
            {
                logger?.Error(e.ToString());
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetRecipeById(int id)
        {
            try
            {
                var recipe = await recipeBll.GetByIdAsync(id);
                return Ok(Mapper.Map<BSRecipe, BSRecipeDto>(recipe));
            }
            catch (Exception e)
            {
                logger?.Error(e.ToString());
            }
            return BadRequest();
        }

        [HttpPost]
        public IHttpActionResult CreateRecipe(BSRecipeDto recipeDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var recipe = Mapper.Map<BSRecipeDto, BSRecipe>(recipeDto);

                recipeBll.Insert(recipe);

                return Created(new Uri($"{Request.RequestUri}/{recipe.Id}"), recipeDto);
            }
            catch (Exception e)
            {
                logger.Error(e.ToString());
            }
            return BadRequest();
        }

        [HttpPut]
        public IHttpActionResult UpdateRecipe(BSRecipeDto recipeDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var recipe = recipeBll.GetById(recipeDto.Id);
                if (recipe == null)
                {
                    return NotFound();
                }

                Mapper.Map(recipeDto, recipe);

                recipeBll.Update(recipe);

                return Ok();
            }
            catch (Exception e)
            {
                logger.Error(e.ToString());
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRecipe(int id)
        {
            try
            {
                var recipe = await recipeBll.GetByIdAsync(id);
                if (recipe == null)
                {
                    return NotFound();
                }
                recipeBll.Delete(recipe);

                return Ok();
            }
            catch (Exception e)
            {
                logger.Error(e.ToString());
            }
            return BadRequest();
        }




    }
}
