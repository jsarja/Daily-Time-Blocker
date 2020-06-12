using System;

namespace Planner.Application.TodoManagement.Dtos
{
    public class DailyTodoItemBlockDto
    {
        public int Id { get; set; }
        public DailyTodoItemDto DTodoItem { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsCompleted { get; set; }
    }
}