using System;
using SQLiteNetExtensions.Attributes;

namespace BusyList.Models
{
    /// <summary>
    /// A model that represents an individual task on a <c>TodoList</c>
    ///
    /// This class extends <c>EntityBase</c> and therefore is a separate table
    /// in the local database
    /// </summary>
    public class TodoItem : EntityBase
    {
        /// <summary>
        /// Id of the list this item belongs to
        /// </summary>
        [ForeignKey(typeof(TodoList))]
        public int ListId { get; set; }
        /// <summary>
        /// Boolean indicating whether or not this task has been completed
        /// </summary>
        public bool IsCompleted { get; set; }
        /// <summary>
        /// Name of the task/task description/title
        /// </summary>
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            //the object reference can be different, but the two items will be
            //considered the same if their ids match
            if (obj != null && obj is TodoItem item)
                return Id == item.Id;
            return base.Equals(obj);
        }
    }
}
