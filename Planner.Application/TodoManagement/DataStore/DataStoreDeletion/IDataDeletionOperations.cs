using System.Threading.Tasks;
using Planner.Application.TodoManagement.DataStore.Core;

namespace Planner.Application.TodoManagement.DataStore.DataStoreDeletion
{
    public interface IDataDeletionOperations : IDataOperations
    {
        public Task TodoItemDeletionAsync(int id);
        public Task DailyTodoItemDeletionAsync(int id);
        public Task DailyTodoItemBlockDeletionAsync(int id);
        public Task TodoItemCategoryDeletionAsync(int id);
    }
}