using System.Threading.Tasks;
using Planner.Application.TodoManagement.DataStore.Core;
using Planner.Domain.Entities;

namespace Planner.Application.TodoManagement.DataStore.DataStoreModification
{
    public interface IDataModificationOperations : IDataOperations
    {
        public Task TodoItemModificationAsync(int id, TodoItem item);
        
        public Task DailyTodoItemModificationAsync(int id, DailyTodoItem item);
        
        public Task DailyTodoItemBlockModificationAsync(int id, DailyTodoItemBlock block);
        
        public Task TodoItemCategoryModificationAsync(int id, TodoItemCategory category);
    }
}