using System;
using System.Collections;
using System.Collections.Generic;
using Planner.Application.TodoManagement.Dtos;

namespace Planner.Application.TodoManagement.TodoActions.ReadActions
{
    public class TodoItemsSearchArgs
    {
        public int? CategoryId { get; set; }
        public string StringFieldsContains { get; set; }
        public bool? IsInUserFavorites { get; set; }
    }
    
    public class GetDailyTodoItemsSearchArgs
    {
        public DateTime? Date { get; set; }
    }
    
    public class GetDailyTodoItemBlocksSearchArgs
    {
        public DateTime? Date { get; set; }
        public DateTime? TimeIntervalStart { get; set; }
        public DateTime? TimeIntervalEnd { get; set; }
        public bool? IsCompleted { get; set; }
    }
    
    public class TodoItemCategoriesSearchArgs
    {
        public int? TodoItemId { get; set; }
        public string StringFieldsContains { get; set; }
    }
    
    public interface IReadActions
    {
        public TodoItemDto GetTodoItem(int id);
        public IEnumerable<TodoItemDto> GetTodoItems(TodoItemsSearchArgs searchArgs);

        public DailyTodoItemDto GetDailyTodoItem(int id);
        public IEnumerable<DailyTodoItemDto> GetDailyTodoItems(GetDailyTodoItemsSearchArgs searchArgs);
        
        public DailyTodoItemBlockDto GetDailyTodoItemBlock(int id);
        public IEnumerable<DailyTodoItemBlockDto> GetDailyTodoItemBlocks(GetDailyTodoItemBlocksSearchArgs searchArgs);
        
        public TodoItemCategoryDto GetTodoItemCategory(int id);
        public IEnumerable<TodoItemCategoryDto> GetTodoItemCategories(TodoItemCategoriesSearchArgs searchArgs);
    }
}