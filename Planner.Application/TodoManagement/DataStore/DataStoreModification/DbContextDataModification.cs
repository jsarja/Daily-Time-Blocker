using System.Threading.Tasks;
using Planner.Application.Common.Interfaces;
using Planner.Application.TodoManagement.DataStore.Core;
using Planner.Domain.Entities;

namespace Planner.Application.TodoManagement.DataStore.DataStoreModification
{
    public class DbContextDataModification : DbContextOperationsBase, IDataModificationOperations
    {
        public DbContextDataModification(IApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task TodoItemModificationAsync(int id, TodoItem item)
        {
            return Task.CompletedTask;
        }

        public Task DailyTodoItemModificationAsync(int id, DailyTodoItem item)
        {
            return Task.CompletedTask;
        }

        public Task DailyTodoItemBlockModificationAsync(int id, DailyTodoItemBlock block)
        {
            return Task.CompletedTask;
        }

        public Task TodoItemCategoryModificationAsync(int id, TodoItemCategory category)
        {
            return Task.CompletedTask;
        }
    }
}