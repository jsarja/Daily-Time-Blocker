using System.Collections.Generic;
using System.Threading.Tasks;
using Planner.Application.TodoManagement.DataStore.Core;
using Planner.Application.TodoManagement.TodoActions.ReadActions;
using Planner.Domain.Entities;

namespace Planner.Application.TodoManagement.DataStore.DataStoreQuery
{
    public interface IDataQueryOperations : IDataOperations
    {
        public Task<TodoItem> TodoItemQueryAsync(int id);
        public Task<IEnumerable<TodoItem>> TodoItemsQueryAsync(TodoItemsSearchArgs searchArgs);
        
        public Task<DailyTodoItem> DailyTodoItemQueryAsync(int id);
        public Task<IEnumerable<DailyTodoItem>> DailyTodoItemsQueryAsync(GetDailyTodoItemsSearchArgs searchArgs);
        
        public Task<DailyTodoItemBlock> DailyTodoItemBlockQueryAsync(int id);
        public Task<IEnumerable<DailyTodoItemBlock>> DailyTodoItemBlocksQueryAsync(GetDailyTodoItemBlocksSearchArgs searchArgs);
        
        public Task<TodoItemCategory> TodoItemCategoryQueryAsync(int id);
        public Task<IEnumerable<TodoItemCategory>> TodoItemCategoriesQueryAsync(TodoItemCategoriesSearchArgs searchArgs);
    }
}