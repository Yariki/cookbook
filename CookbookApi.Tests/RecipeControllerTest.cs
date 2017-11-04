using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using AutoMapper;
using Cookbook.BussinessLayer.Interfaces;
using Cookbook.Data.Models;
using CookbookApi.Controllers;
using CookbookApi.Dto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ninject;

namespace CookbookApi.Tests
{
    [TestClass]
    public class RecipeControllerTest
    {
        
        private Mock<IBSRecipeBll> MockRecipeBll { get; set; }

        private Mock<IBSEntityHistoryBll> MockEntityHistoryBll { get; set; }

        private IKernel Kernel { get; set; }
        

        [TestInitialize]
        public void InnitializeTest()
        {
            MockRecipeBll = new Mock<IBSRecipeBll>();
            MockEntityHistoryBll = new Mock<IBSEntityHistoryBll>();
            Mapper.Initialize(m => m.AddProfile(typeof(MappingProfile)));
            Kernel = new StandardKernel();
            Kernel.Load(typeof(CookbookApiModule).Assembly);
            Kernel.Bind<IBSRecipeBll>().ToConstant(MockRecipeBll.Object);
            Kernel.Bind<IBSEntityHistoryBll>().ToConstant(MockEntityHistoryBll.Object);
        }
        
        [TestMethod]
        public void RecipeController_GetAll_Test()
        {
            MockRecipeBll.Setup(b => b.GetAll(It.IsAny<string[]>())).Returns((string[] inc) =>
            {
                return new List<BSRecipe>()
                {
                    new BSRecipe(),
                    new BSRecipe()
                };
            });

            var controller = Kernel.Get<RecipeController>();
            var result = controller.GetAllRecipes() as OkNegotiatedContentResult<IEnumerable<BSRecipeDto>>;
            
            Assert.IsNotNull(result,"The list of recipies is null.");
            Assert.AreEqual(2,result.Content.Count(),"The count is not expected.");
        }

        [TestMethod]
        public void RecipeController_GetRecipeById_Test()
        {
            var data = CreateData();
            MockRecipeBll.Setup(b => b.GetById(It.IsAny<int>())).Returns((int id) =>
            {
                return data.FirstOrDefault(r => r.Id == id);
            });

            var controller = Kernel.Get<RecipeController>();
            var result = controller.GetRecipeById(1) as OkNegotiatedContentResult<BSRecipeDto>;

            Assert.IsNotNull(result, "The result is null.");
            Assert.IsNotNull(result.Content, "The recipe is null.");
            Assert.AreEqual(1, result.Content.Id, "The id's is not equal.");
        }
        
        private List<BSRecipe> CreateData()
        {
            return new List<BSRecipe>()
            {
                new BSRecipe(){Id = 1},
                new BSRecipe() {Id = 2}
            };
        }
    }
}
