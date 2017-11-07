using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mime;
using System.Threading.Tasks;
using Cookbook.Data.Context;
using Cookbook.Data.Interfaces;

namespace Cookbook.Data.Core
{
    public class BSCoreRepository<TEntity> : IBSCoreRepository<TEntity> where TEntity : BSCoreEntity
    {
        private CookbookDbContext context;
        private DbSet<TEntity> set;

        public BSCoreRepository(CookbookDbContext context)
        {
            this.context = context;
            set = this.context.Set<TEntity>();
        }

        public DbSet<TEntity> Set => set;


        public virtual IEnumerable<TEntity> GetAll(params string[] includes)
        {
            IQueryable<TEntity> qset = set;
            if (includes != null && includes.Length > 0)
            {
                foreach (var s in includes)
                {
                    qset = qset.Include(s);
                }
            }
            return qset.ToList();
        }

        public virtual IEnumerable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> filter, params string[] includes)
        {
            IQueryable<TEntity> query = set;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includes != null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.ToList();

        }

        public TEntity GetById(int id)
        {
            return Set.Include("Ingredients").SingleOrDefault(r => r.Id == id);
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await set.Include("Ingredients").SingleOrDefaultAsync(r => r.Id == id);
        }

        public virtual void Insert(TEntity entity)
        {
            if (entity == null)
            {
                return;
            }
            entity.Created = DateTime.Now;
            set.Add(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                set.Attach(entity);
            }
            set.Remove(entity);
        }

        public virtual void Update(TEntity entity)
        {
            Set.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }
        

    }
}