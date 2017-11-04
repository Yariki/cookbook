using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Cookbook.BussinessLayer.Interfaces;
using Cookbook.Data.Context;
using Cookbook.Data.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ninject;

namespace Cookbook.BussinessLayer.Test.Base
{
    public class BSBaseBllTest
    {
        private IKernel kernel;

        private void CreateTestNinjectKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(typeof(IBSCoreBll<>).Assembly);
        }

        private Mock<IBSContextFactory> ContextFactoryMock { get; set; }
        protected Mock<CookbookDbContext> ContextMock { get; set; }
        protected Mock<IBSUnitOfWork> UnitOfWorkMock { get; set; }

        #region configure test

        [TestInitialize]
        public void TestInitialize()
        {
            ContextMock = new Mock<CookbookDbContext>();
            UnitOfWorkMock = new Mock<IBSUnitOfWork>();

            ContextFactoryMock = new Mock<IBSContextFactory>();
            ContextFactoryMock.Setup(f => f.CreateContext<CookbookDbContext>()).Returns(ContextMock.Object);
            ContextFactoryMock.Setup(f => f.CreateUnitOfWork(ContextMock.Object)).Returns(UnitOfWorkMock.Object);

            CreateTestNinjectKernel();
        }

        #endregion

        protected IBSContextFactory ContextFactory => ContextFactoryMock.Object;


        private Mock<IBSCoreRepository<T>> CreateRepositoryMock<T>(IEnumerable<T> data) where T : class, IBSCoreEntity
        {
            var dbSetMock = DBSetMock(data.ToList());

            var repositoryMock = new Mock<IBSCoreRepository<T>>();
            repositoryMock.Setup(r => r.Insert(It.IsAny<T>())).Callback((T item) => dbSetMock.Object.Add(item));
            repositoryMock.Setup(r => r.Delete(It.IsAny<T>())).Callback((T item) => dbSetMock.Object.Remove(item));
            repositoryMock.Setup(r => r.Update(It.IsAny<T>())).Callback((T item) => dbSetMock.Object.Attach(item));
            repositoryMock.Setup(r => r.Delete(It.IsAny<T>())).Callback((T item) => dbSetMock.Object.Remove(item));
            repositoryMock.Setup(r => r.GetAll(It.IsAny<string[]>())).Returns(dbSetMock.Object.ToList());

            return repositoryMock;
        }

        private Mock<DbSet<T>> DBSetMock<T>(List<T> data) where T : class, IBSCoreEntity
        {
            var dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.AsQueryable().Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.AsQueryable().Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.AsQueryable().ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.AsQueryable().GetEnumerator());
            dbSetMock.Setup(d => d.Include(It.IsAny<string>())).Returns(dbSetMock.Object);
            dbSetMock.Setup(set => set.Add(It.IsAny<T>())).Callback<T>(data.Add);
            dbSetMock.Setup(set => set.Attach(It.IsAny<T>())).Callback(
                (T item) =>
                {
                    var itemIndex = data.FindIndex(c => c.Id == item.Id);
                    data[itemIndex] = item;
                });
            dbSetMock.Setup(set => set.Remove(It.IsAny<T>())).Callback((T item) =>
            {
                data.Remove(item);
            });
            dbSetMock.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => data.FirstOrDefault(d => d.Id == (int)ids[0]));

            ContextMock.Setup(c => c.Set<T>()).Returns(dbSetMock.Object);
            return dbSetMock;
        }

        protected Mock<IBSCoreRepository<TEntity>> MockRepository<TEntity>(params TEntity[] entities) where TEntity : class, IBSCoreEntity
        {
            return MockRepository(entities.AsQueryable());
        }

        protected Mock<IBSCoreRepository<TEntity>> MockRepository<TEntity>(List<TEntity> entities) where TEntity : class, IBSCoreEntity
        {
            return MockRepository(entities.AsQueryable());
        }

        protected Mock<IBSCoreRepository<TEntity>> MockRepository<TEntity>(IQueryable<TEntity> entities) where TEntity : class, IBSCoreEntity
        {
            var repositoryMock = CreateRepositoryMock(entities);
            UnitOfWorkMock.Setup(c => c.Repository<TEntity>()).Returns(repositoryMock.Object);
            return repositoryMock;
        }


    }
}