using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Abstractions
{
    /// <summary>
    /// Should be implemented by all classes to be mapped to a DB object.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Entity Primary Key.
        /// </summary>
        int Id { get; set; }


    }
}
