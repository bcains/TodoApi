namespace TodoApi.Models
{
    /// <summary>
    /// A todo item.</summary>
    public class TodoItem
    {
        /// <summary>
        /// GUID of the todo item.</summary>
        public string Key { get; set; }

        /// <summary>
        /// Name of the todo item.</summary>
        public string Name { get; set; }

        /// <summary>
        /// Status of the todo item.</summary>
        public bool IsComplete { get; set; }
    }
}
