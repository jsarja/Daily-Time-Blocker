using System.Collections.Generic;

namespace Planner.Application.TodoManagement.Dtos
{
    public class TodoItemCategoryDto
    {
        public int Id { get; set; }
        public IEnumerable<TodoItemDto> Todos { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int OwnerId { get; set; }
    }
}