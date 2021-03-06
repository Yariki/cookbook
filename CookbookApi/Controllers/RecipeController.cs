﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Cookbook.BussinessLayer.Interfaces;
using Cookbook.Data.Core;
using Cookbook.Data.Models;
using CookbookApi.Dto;
using CookbookApi.Interfaces;
using Ninject;

namespace CookbookApi.Controllers
{
    public class RecipeController : ApiController
    {
        private IBSRecipeBll recipeBll;
        private IBSEntityHistoryBll historyBll;
        
        
        public RecipeController(IBSRecipeBll recipeBll,IBSEntityHistoryBll historyBll)
        {
            this.recipeBll = recipeBll;
            this.historyBll = historyBll;
        }

        [Inject]
        public IBSLogger Logger { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }


        [HttpGet]
        public IHttpActionResult GetAllRecipes()
        {
            try
            {
                var recipes = recipeBll.GetAll();
                var dtos = recipes.Select(Mapper.Map<BSRecipe, BSRecipeDto>);
                return Ok(dtos);
            }
            catch (Exception e)
            {
                Logger?.Error(e.ToString());
            }
            return BadRequest();
        }

        [HttpGet]
        public IHttpActionResult GetRecipeById(int id)
        {
            try
            {
                var recipe = recipeBll.GetById(id);
                return Ok(Mapper.Map<BSRecipe, BSRecipeDto>(recipe));
            }
            catch (Exception e)
            {
                Logger?.Error(e.ToString());
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
                Logger.Error(e.ToString());
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

                historyBll.AddHistory(recipe);
                
                var mappedRecipient = Mapper.Map<BSRecipe>(recipeDto);
                
                recipeBll.Update(mappedRecipient);

                return Ok();
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
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
                Logger.Error(e.ToString());
            }
            return BadRequest();
        }

        



    }
}
