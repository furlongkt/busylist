using System;
using SQLite;

namespace BusyList.Models
{
    /// <summary>
    /// Entities are tables in the local database. This class is 
    /// the base class for all entities. Any class that extends this one will
    /// automatically have a table created in the local database.
    ///
    /// Be sure to create a new <c>Proxy</c>/<c>Manager</c> when applicable and
    /// do not interact directly with the proxy.
    /// </summary>
    public class EntityBase : IEntity
    {
        /// <summary>
        /// Auto incrementing unique id.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int? Id { get; set; }
    }
}
