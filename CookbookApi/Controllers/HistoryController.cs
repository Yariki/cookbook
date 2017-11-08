using System;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Cookbook.BussinessLayer.Interfaces;
using Cookbook.Data.Models;
using CookbookApi.Dto;
using CookbookApi.Interfaces;
using Ninject;
using Ninject.Infrastructure.Language;

namespace CookbookApi.Controllers
{
    public class HistoryController : ApiController
    {
        private IBSEntityHistoryBll historyBll;

        public HistoryController(IBSEntityHistoryBll historyBll)
        {
            this.historyBll = historyBll;
        }

        [Inject]
        public IBSLogger Logger { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        [HttpGet]
        public IHttpActionResult GetHistory(int id)
        {
            try
            {
                var result = historyBll.GetHistoryForEntity<BSRecipe>(id);

                if (result == null || !result.Any())
                {
                    return NotFound();
                }
                var dtos = result.Select(r => Mapper.Map<BSRecipe, BSRecipeDto>(r)).OrderBy(r => r.Created);
                return Ok(dtos);
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
            }
            return BadRequest();
        }
    }
}