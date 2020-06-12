using System;
using Planner.Application.TodoManagement.Dtos;

namespace Planner.Application.TodoManagement.TodoActions.UpdateActions
{
    public class UpdateTodoItemArgs
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool? SaveAsFavorite { get; set; }
    }
    
    public class UpdateDailyTodoItemArgs
    {
        public DateTime? TodoDate { get; set; }
        public TimeSpan? TimeUsedForTodo { get; set; }
        public TimeSpan? TimeReservedForTodo { get; set; }
    }
    
    public class UpdateDailyTodoItemBlockArgs
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool? IsCompleted { get; set; }
    }
    
    public class UpdateTodoItemCategoryArgs
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
    
    public interface IUpdateActions
    {
        public TodoItemDto UpdateTodoItem(UpdateTodoItemArgs item);
        public DailyTodoItemDto UpdateDailyTodoItem(UpdateDailyTodoItemArgs item);
        public DailyTodoItemBlockDto UpdateDailyTodoItemBlock(UpdateDailyTodoItemBlockArgs item);
        
        public TodoItemCategoryDto UpdateTodoItemCategory(UpdateTodoItemCategoryArgs item);
        public TodoItemCategoryDto AddTodoItemToCategory(int categoryId, int itemId);
    }
}