using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Abstractions
{
    public interface IMapper<TEntity, TModel> where TModel : new()
    {
        TEntity MapToEntity(TModel model);

        TModel MapToModel(TEntity entity);

        IEnumerable<TEntity> MapToEntities(IEnumerable<TModel> models);

        IEnumerable<TModel> MapToModels(IEnumerable<TEntity> entities);
    }
}
