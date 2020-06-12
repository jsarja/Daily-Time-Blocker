using System.Collections.Generic;

namespace Planner.Application.TodoManagement.Dtos
{
    public class TodoItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int OwnerId { get; set; }
        public string Description { get; set; }
        public bool IsUserFavorite { get; set; }
        public IEnumerable<TodoItemCategoryDto> Categories { get; set; }
    }
}