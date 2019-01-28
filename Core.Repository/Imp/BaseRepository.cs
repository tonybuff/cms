using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Core.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Core.Repository.Imp
{
    public abstract class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        protected CMSDbContext _dbContext;

        public BaseRepository(CMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> Entities => _dbContext.Set<TEntity>();


        public int Delete(object id, bool isSave = true)
        {
            var entity = _dbContext.Set<TEntity>().Find(id);
            return Delete(entity, isSave);
        }

        public int Delete(TEntity entity, bool isSave = true)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
            return isSave ? _dbContext.SaveChanges() : 0;
        }

        public int Delete(IEnumerable<TEntity> entities, bool isSave = true)
        {
            try
            {
                _dbContext.ChangeTracker.AutoDetectChangesEnabled = false;
                foreach (var entity in entities)
                {
                    Delete(entity);
                }
                return isSave ? _dbContext.SaveChanges() : 0;
            }
            finally
            {
                _dbContext.ChangeTracker.AutoDetectChangesEnabled = true;
            }
        }

        public int Delete(Expression<Func<TEntity, bool>> predicate, bool isSave = true)
        {
            var entities = _dbContext.Set<TEntity>().Where(predicate);
            return Delete(entities, isSave);
        }


        public TEntity GetByKey(object key)
        {
            return _dbContext.Set<TEntity>().Find(key);
        }

        public List<QueryEntity> GetListBySql<QueryEntity>(string sqlString, params object[] parameters) where QueryEntity : class
        {
            return _dbContext.Query<QueryEntity>().FromSql(sqlString, parameters).ToList();
        }

        public IEnumerable<TEntity> GetPages(int page, int size, out int total)
        {
            total = Entities.Count();
            return Entities.OrderByDescending(o => o.CreateDate).Skip((page - 1) * size).Take(size);
        }

        public IEnumerable<TEntity> GetPages(Expression<Func<TEntity, bool>> prodicate, int page, int size, out int total)
        {
            var entities = prodicate != null ? Entities.Where(prodicate) : Entities;
            total = entities.Count();
            return entities.OrderByDescending(o => o.CreateDate).Skip((page - 1) * size).Take(size);
        }

        public int Insert(TEntity entity, bool isSave = true)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            return isSave ? _dbContext.SaveChanges() : 0;
        }

        public int Insert(IEnumerable<TEntity> entities, bool isSave = true)
        {
            try
            {
                _dbContext.ChangeTracker.AutoDetectChangesEnabled = false;
                foreach (var entity in entities)
                {
                    Insert(entity);
                }
                return isSave ? _dbContext.SaveChanges() : 0;
            }
            finally
            {
                _dbContext.ChangeTracker.AutoDetectChangesEnabled = true;
            }
        }

        public int Update(TEntity entity, bool isSave = true)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return isSave ? _dbContext.SaveChanges() : 0;
        }

        public int Update(IEnumerable<TEntity> entities, bool isSave = true)
        {
            try
            {
                _dbContext.ChangeTracker.AutoDetectChangesEnabled = false;
                foreach (var entity in entities)
                {
                    Update(entity,false);
                }
                return isSave ? _dbContext.SaveChanges() : 0;
            }
            finally
            {
                _dbContext.ChangeTracker.AutoDetectChangesEnabled = true;
            }
        }

    }
}
