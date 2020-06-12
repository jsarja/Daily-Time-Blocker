using System.Threading;
using System.Threading.Tasks;
using Planner.Application.TodoManagement.DataStore.Core;
using Planner.Domain.Entities;

namespace Planner.Application.TodoManagement.DataStore.DataStoreInsertion
{
    public class InMemoryDataInsertion : IDataInsertionOperations
    {
        private readonly MockDataRepo m_dataRepo;
        public InMemoryDataInsertion(MockDataRepo dataRepo)
        {
            m_dataRepo = dataRepo;
        }
        
        public Task<bool> CommitAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }

        public Task<int> TodoItemInsertionAsync(TodoItem item)
        {
            m_dataRepo.TodoItems.Add(item);
            return Task.FromResult(item.TodoItemId);
        }

        public Task<int> DailyTodoItemInsertionAsync(DailyTodoItem item)
        {
            m_dataRepo.DailyTodoItems.Add(item);
            return Task.FromResult(item.DailyTodoItemId);
        }

        public Task<int> DailyTodoItemBlockInsertionAsync(DailyTodoItemBlock block)
        {
            m_dataRepo.DailyTodoItemBlocks.Add(block);
            return Task.FromResult(block.DailyTodoItemBlockId);
        }

        public Task<int> TodoItemCategoryInsertionAsync(TodoItemCategory category)
        {
            m_dataRepo.TodoItemCategories.Add(category);
            return Task.FromResult(category.TodoItemCategoryId);
        }

        public Task TodoItemToCategoryInsertionAsync(int categoryId, int itemId)
        {
            var item = m_dataRepo.TodoItems.Find(i => i.TodoItemId == itemId);
            var category = m_dataRepo.TodoItemCategories
                .Find(c => c.TodoItemCategoryId == categoryId);
            
            item.Categories.Add(category);
            category.TodoCollection.Add(item);
            return Task.CompletedTask;
        }
    }
}