using System;
using System.Collections.Generic;
using SQLiteNetExtensions.Attributes;

namespace BusyList.Models
{
    /// <summary>
    /// A model that represents an individual todo list.
    ///
    /// This class extends <c>EntityBase</c> and therefore is a separate table
    /// in the local database
    /// </summary>
    public class TodoList : EntityBase
    {
        /// <summary>
        /// Title of the list
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Subtitle of the list
        /// </summary>
        public string Subtitle { get; set; }
        /// <summary>
        /// Indicator color of the list. Defaults to <c>#8c7ae6</c>
        /// </summary>
        public string Color { get; set; } = "#8c7ae6";
        /// <summary>
        /// Tasks attached to the list
        /// </summary>
        [OneToMany]
        public List<TodoItem> Items { get; set; }

        public override bool Equals(object obj)
        {
            //the object reference can be different, but the two items will be
            //considered the same if their ids match
            if (obj is TodoList list)
                return list.Id == Id;

            return base.Equals(obj);
        }
    }
}
