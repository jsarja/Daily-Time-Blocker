using System;
using Planner.Application.TodoManagement.Dtos;
using Planner.Domain.Entities;

namespace Planner.Application.TodoManagement.TodoActions.CreateActions
{
    public class CreateTodoItemArgs
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int OwnerId { get; set; }
        public bool? SaveAsFavorite { get; set; }
    }
    
    public class CreateDailyTodoItemArgs
    {
        public int TodoItemId { get; set; }
        public DateTime TodoDate { get; set; }
        public TimeSpan? TimeReservedForTodo { get; set; }
    }
    
    public class CreateDailyTodoItemBlockArgs
    {
        public int DailyTodoItemId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool? IsCompleted { get; set; }
    }
    
    public class CreateTodoItemCategoryArgs
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int OwnerId { get; set; }
    }
    
    public interface ICreateTodoItems
    {
        public TodoItemDto CreateTodoItem(CreateTodoItemArgs itemData);
        public DailyTodoItemDto CreateDailyTodoItem(CreateDailyTodoItemArgs dailyItemData);
        public DailyTodoItemBlockDto CreateDailyTodoItemBlock(CreateDailyTodoItemBlockArgs blockData);
        public DailyTodoItemBlockDto CreateDailyTodoItemBlockFromScratch(
            CreateTodoItemArgs itemData,
            CreateDailyTodoItemArgs dailyItemData, 
            CreateDailyTodoItemBlockArgs blockData);
        public TodoItemCategoryDto CreateTodoItemCategory(CreateTodoItemCategoryArgs item);
    }
}