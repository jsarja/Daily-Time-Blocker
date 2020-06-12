using Planner.Domain.Entities;

namespace Planner.Application.Common.Models
{
    public class TodoItemCategoryJoinTable
    {
        public TodoItem Item { get; set; }
        public int TodoItemId { get; set; } //=> Item.TodoItemId;
        public TodoItemCategory Category { get; set; }
        public int CategoryId { get; set; } //=> Category.TodoItemCategoryId;
    }
}