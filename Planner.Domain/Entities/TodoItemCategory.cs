using System.Collections;
using System.Collections.Generic;

namespace Planner.Domain.Entities
{
    public class TodoItemCategory
    {
        public TodoItemCategory()
        {
            TodoItemSet = new HashSet<TodoItem>();
        }
        public int TodoItemCategoryId { get; set; }
        
        public ICollection<TodoItem> TodoItemSet { get; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int OwnerId { get; set; }
    }
}