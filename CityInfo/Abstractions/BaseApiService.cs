using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.Abstractions
{
    /// <inheritdoc/>
    public abstract class BaseApiService<TEntity, TModel> : IApiService<TModel>
        where
        TEntity : class, IEntity
        where
        TModel : new()
    {

        protected CityInfoContext Db;

        protected IMapper<TEntity, TModel> Mapper;

        protected virtual IQueryable<TEntity> ManySelector => Db.Set<TEntity>();

        protected virtual IQueryable<TEntity> SingleSelector => Db.Set<TEntity>();

        protected BaseApiService(
            CityInfoContext db,
            IMapper<TEntity, TModel> mapper
        )
        {
            Db = db;
            Mapper = mapper;
        }

        public virtual IEnumerable<TModel> Get()
        {
            var entities = ManySelector.ToList();
            return Mapper.MapToModels(entities);
        }

        public virtual TModel GetById(int id, bool asNoTracking = false)
        {
            TEntity entity;

            if (asNoTracking)
            {
                entity = SingleSelector.AsNoTracking().SingleOrDefault(e => e.Id == id);
            }
            else
            {
                entity = SingleSelector.SingleOrDefault(e => e.Id == id);
            }

            if (entity == null)
            {
                return default(TModel);
            }

            return Mapper.MapToModel(entity);
        }

        public virtual TModel Update(TModel model)
        {
            var entity = Mapper.MapToEntity(model);
            Db.Set<TEntity>().Update(entity);
            Db.SaveChanges();

            model = Mapper.MapToModel(entity);

            return model;
        }

        public virtual int Create(TModel model)
        {
            var entity = Mapper.MapToEntity(model);
            Db.Set<TEntity>().Add(entity);
            Db.SaveChanges();

            return entity.Id;
        }

        public virtual void Delete(int id)
        {
            var entity = Db.Set<TEntity>().Find(id);
            if (entity == null)
            {
                throw new Exception(System.Net.HttpStatusCode.NotFound.ToString());
            }
            Db.Set<TEntity>().Remove(entity);
            Db.SaveChanges();
        }

        public virtual IEnumerable<int> Create(List<TModel> models)
        {
            var entities = Mapper.MapToEntities(models).ToList();
            Db.Set<TEntity>().AddRange(entities);
            Db.SaveChanges();
            return entities.Select(x => x.Id);
        }
    }
}
