using System.Threading.Tasks;
using Planner.Application.TodoManagement.DataStore.Core;
using Planner.Domain.Entities;

namespace Planner.Application.TodoManagement.DataStore.DataStoreInsertion
{
    public interface IDataInsertionOperations : IDataOperations
    {
        public Task<int> TodoItemInsertionAsync(TodoItem item);
        
        public Task<int> DailyTodoItemInsertionAsync(DailyTodoItem item);
        
        public Task<int> DailyTodoItemBlockInsertionAsync(DailyTodoItemBlock block);
        
        public Task<int> TodoItemCategoryInsertionAsync(TodoItemCategory category);
        
        public Task TodoItemToCategoryInsertionAsync(int categoryId, int itemId);
    }
}