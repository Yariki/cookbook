using System;
using System.Collections.Generic;
using System.Linq;
using Cookbook.BussinessLayer.Blls;
using Cookbook.BussinessLayer.Test.Base;
using Cookbook.Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cookbook.BussinessLayer.Test
{
    [TestClass]
    public class BSRecipeBllTest : BSBaseBllTest
    {

        [TestMethod]
        public void BSRecipeBLL_GetAll_Text()
        {
            var list = CreateData();

            MockRepository(list);

            var recipBll = new BSRecipeBll(ContextFactory);
            var result = recipBll.GetAll();

            Assert.AreEqual(2,result.Count(),"Different count of recipes.");
        }

        [TestMethod]
        public void BSRecipeBLL_Insert_Test()
        {
            var data = CreateData();
            MockRepository(data);
            var recipBll = new BSRecipeBll(ContextFactory);
            var recipe = new BSRecipe(){Id = 3, Name = "Recipe 3"};
            recipBll.Insert(recipe);
            var result = recipBll.GetById(recipe.Id);
            Assert.IsNotNull(result,"Is Null");
        }
        
        private List<BSRecipe> CreateData()
        {
            return new List<BSRecipe>()
            {
                new BSRecipe()
                {
                    Id = 1,
                    Name = "Recipe 1",
                    Description = "Decs 1",
                    Created = DateTime.Now
                },
                new BSRecipe()
                {
                    Id = 2,
                    Name = "Recipe 2",
                    Description = "Decs 2",
                    Created = DateTime.Now
                }
            };
        }


    }
}
