using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace TodoApi.Models
{
    /// <summary>
    /// Repository for todo list items. </summary>
    public class TodoRepository : ITodoRepository
    {
        /// <summary>
        /// Dictionary for storing todos. </summary>
        private static ConcurrentDictionary<string, TodoItem> _todos = new ConcurrentDictionary<string, TodoItem>();

        /// <summary>
        /// Class constructor. </summary>
        public TodoRepository()
        {
            // Use Add to initialize the todo list with some default items, if desired.
        }

        /// <summary>
        /// Add item to todo list. </summary>
        /// <param name="item"> TodoItem to be added.</param>
        public void Add(TodoItem item)
        {
            item.Key = Guid.NewGuid().ToString();
            _todos[item.Key] = item;
        }

        /// <summary>
        /// Find and return an item using the item key. </summary>
        /// <param name="key"> GUID for the todo item.</param>
        public TodoItem Find(string key)
        {
            TodoItem item;
            _todos.TryGetValue(key, out item);
            return item;
        }

        /// <summary>
        /// Find and return all items. </summary>
        public IEnumerable<TodoItem> GetAll()
        {
            return _todos.Values;
        }

        /// <summary>
        /// Remove item from todo list. </summary>
        /// <param name="key"> GUID for the todo item.</param>
        public TodoItem Remove(string key)
        {
            TodoItem item;
            _todos.TryGetValue(key, out item);
            _todos.TryRemove(key, out item);
            return item;
        }

        /// <summary>
        /// Update existing item in the todo list. </summary>
        /// <param name="item"> TodoItem with the same key as the item to be updated.</param>
        public void Update(TodoItem item)
        {
            _todos[item.Key] = item;
        }
    }
}
