using System.Collections;
using System.Collections.Generic;

namespace Planner.Domain.Entities
{
    public class TodoItemCategory
    {
        public TodoItemCategory()
        {
            TodoCollection = new List<TodoItem>();
        }
        public int TodoItemCategoryId { get; set; }
        
        public ICollection<TodoItem> TodoCollection { get; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int OwnerId { get; set; }
    }
}