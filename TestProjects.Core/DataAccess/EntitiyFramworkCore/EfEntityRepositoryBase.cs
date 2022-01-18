using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestProjects.Core.Entities;

namespace TestProjects.Core.DataAccess.EntitiyFramworkCore
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntitiyRepository<TEntity>
        where TEntity : class, IEntitiy, new()
        where TContext: DbContext,new()
    {
        public TEntity Add(TEntity entity)
        {
            using (var context=new TContext())
            {
                var addEntity=context.Entry(entity);
                addEntity.State = EntityState.Added;
                context.SaveChanges();
                return entity;
            }
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            using (var context=new TContext())
            {
                var addEntity = context.Entry(entity);
                addEntity.State= EntityState.Added;
                await  context.SaveChangesAsync();
                return entity;
            }
        }

        public void Delete(TEntity entity)
        {
            using (var context=new TContext())
            {
                var deleteEntity=context.Entry(entity);
                deleteEntity.State=EntityState.Deleted;
                context.SaveChanges();
                
            }
        }

        public async Task DeleteAsync(TEntity entity)
        {
            using(var context=new TContext())
            {
                var deleteEntity = context.Entry(entity);
                deleteEntity.State = EntityState.Deleted;
                await context.SaveChangesAsync();
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context=new TContext())
            {
                return filter == null
                    ? context.Set<TEntity>().ToList()
                    :context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public TEntity GetT(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public TEntity Update(TEntity entity)
        {
            using (var context=new TContext())
            {
                var updateEntity=context.Entry(entity);
                updateEntity.State=EntityState.Modified;
                context.SaveChanges();
                return entity;
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            using (var context = new TContext())
            {
                var updateEntity = context.Entry(entity);
                updateEntity.State = EntityState.Modified;
                await context.SaveChangesAsync();
                return entity;
            }
        }
    }
}
