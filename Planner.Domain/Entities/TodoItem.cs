using System.Collections.Generic;

namespace Planner.Domain.Entities
{
    public class TodoItem
    {
        public TodoItem()
        {
            CategorySet = new HashSet<TodoItemCategory>();
        }
        public int TodoItemId { get; set; }
        
        public string Title { get; set; }
        public int OwnerId { get; set; }
        public string Description { get; set; }
        public bool IsUserFavorite { get; set; }
        
        public ICollection<TodoItemCategory> CategorySet { get; }
    }
}