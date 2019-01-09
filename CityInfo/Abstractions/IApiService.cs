using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Abstractions
{
    public interface IApiService<TInput, TOutput>
    {
        /// <summary>
        /// Returns all the entities transformed to the specifeid ViewModel.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TOutput> Get();

        /// <summary>
        /// Searchs an entity by its Primary Key and returns it trasnformed into the specified ViewModel.
        /// </summary>
        /// <param name="id">Entity Primary Key.</param>
        /// <returns></returns>
        TOutput GetById(int id, bool asNoTracking = false);

        /// <summary>
        /// Updates an entity in the DB.
        /// </summary>
        /// <param name="model">The model to be converted and updated in the DB.</param>
        /// <returns></returns>
        TOutput Update(TInput model);

        /// <summary>
        /// Inserts a new entity in the DB.
        /// </summary>
        /// <param name="model">The model to be converted and inserted in the DB.</param>
        /// <returns></returns>
        int Create(TInput model);

        /// <summary>
        /// Deletes an entity by its primary key.
        /// </summary>
        /// <param name="id">Entity Primary Key.</param>
        void Delete(int id);

        /// <summary>
        /// Inserts new entities in the DB.
        /// </summary>
        /// <param name="models">The models to be converted and inserted in the DB.</param>
        /// <returns></returns>
        IEnumerable<int> Create(List<TInput> models);

    }

    /// <inheritdoc cref="IApiService{TInput,TOutput}"/>
    public interface IApiService<TModel> : IApiService<TModel, TModel>
    {

    }
}
