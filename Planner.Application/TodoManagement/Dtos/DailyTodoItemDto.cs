using System;
using Planner.Domain.Entities;

namespace Planner.Application.TodoManagement.Dtos
{
    public class DailyTodoItemDto
    {
        public int Id { get; set; }
        public TodoItemDto TodoInfo { get; set; }
        public DateTime TodoDate { get; set; }
        public TimeSpan TimeUsedForTodo { get; set; }
        public TimeSpan? TimeReservedForTodo { get; set; }
    }
}