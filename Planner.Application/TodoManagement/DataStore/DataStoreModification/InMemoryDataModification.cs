using System.Threading;
using System.Threading.Tasks;
using Planner.Application.TodoManagement.DataStore.Core;
using Planner.Domain.Entities;

namespace Planner.Application.TodoManagement.DataStore.DataStoreModification
{
    public class InMemoryDataModification : IDataModificationOperations
    {
        private readonly MockDataRepo m_dataRepo;
        public InMemoryDataModification(MockDataRepo dataRepo)
        {
            m_dataRepo = dataRepo;
        }

        public Task<bool> CommitAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }

        public Task TodoItemModificationAsync(int id, TodoItem item)
        {
            var repoItem = m_dataRepo.TodoItems.Find(i => i.TodoItemId == id);
            if (repoItem == null)
            {
                return Task.CompletedTask;
            }
            
            repoItem.Title = item.Title;
            repoItem.Description = item.Description;
            repoItem.IsUserFavorite = item.IsUserFavorite;
            
            return Task.CompletedTask;
        }

        public Task DailyTodoItemModificationAsync(int id, DailyTodoItem item)
        {
            var repoItem = m_dataRepo.DailyTodoItems.Find(i => i.DailyTodoItemId == id);
            if (repoItem == null)
            {
                return Task.CompletedTask;
            }
            
            repoItem.TodoDate = item.TodoDate;
            repoItem.TimeUsedForTodo = item.TimeUsedForTodo;
            repoItem.TimeReservedForTodo = item.TimeReservedForTodo;
            
            return Task.CompletedTask;
        }

        public Task DailyTodoItemBlockModificationAsync(int id, DailyTodoItemBlock block)
        {
            var repoItem = m_dataRepo.DailyTodoItemBlocks
                .Find(i => i.DailyTodoItemBlockId == id);
            if (repoItem == null)
            {
                return Task.CompletedTask;
            }
            
            repoItem.StartTime = block.StartTime;
            repoItem.EndTime = block.EndTime;
            repoItem.IsCompleted = block.IsCompleted;
            
            return Task.CompletedTask;
        }

        public Task TodoItemCategoryModificationAsync(int id, TodoItemCategory category)
        {
            var repoItem = m_dataRepo.TodoItemCategories.Find(i => i.TodoItemCategoryId == id);
            if (repoItem == null)
            {
                return Task.CompletedTask;
            }
            
            repoItem.Title = category.Title;
            repoItem.Description = category.Description;

            return Task.CompletedTask;
        }
    }
}