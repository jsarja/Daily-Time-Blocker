using System;
using System.Threading;
using System.Threading.Tasks;
using Planner.Application.TodoManagement.DataStore.Core;
using Planner.Domain.Entities;

namespace Planner.Application.TodoManagement.DataStore.DataStoreInsertion
{
    public class InMemoryDataInsertion : IDataInsertionOperations
    {
        private readonly MockDataRepo m_dataRepo;
        private bool _success = true;
        public InMemoryDataInsertion(MockDataRepo dataRepo)
        {
            m_dataRepo = dataRepo;
        }
        
        public Task<bool> CommitAsync(CancellationToken cancellationToken)
        {
            var success = _success;
            _success = true;
            return Task.FromResult(success);
        }

        public Task<int> TodoItemInsertionAsync(TodoItem item)
        {
            var _ = item != null ? "" : throw new ArgumentNullException();
            
            m_dataRepo.TodoItems.Add(item);
            return Task.FromResult(item.TodoItemId);
        }

        public Task<int> DailyTodoItemInsertionAsync(DailyTodoItem item)
        {
            var _ = item != null ? "" : throw new ArgumentNullException();
            
            m_dataRepo.DailyTodoItems.Add(item);
            return Task.FromResult(item.DailyTodoItemId);
        }

        public Task<int> DailyTodoItemBlockInsertionAsync(DailyTodoItemBlock block)
        {
            var _ = block != null ? "" : throw new ArgumentNullException();
            
            m_dataRepo.DailyTodoItemBlocks.Add(block);
            return Task.FromResult(block.DailyTodoItemBlockId);
        }

        public Task<int> TodoItemCategoryInsertionAsync(TodoItemCategory category)
        {
            var _ = category != null ? "" : throw new ArgumentNullException();
            
            m_dataRepo.TodoItemCategories.Add(category);
            return Task.FromResult(category.TodoItemCategoryId);
        }

        public Task TodoItemToCategoryInsertionAsync(int categoryId, int itemId)
        {
            var item = m_dataRepo.TodoItems.Find(i => i.TodoItemId == itemId);
            var category = m_dataRepo.TodoItemCategories
                .Find(c => c.TodoItemCategoryId == categoryId);

            if (item == null || category == null)
            {
                _success = false;
                return Task.CompletedTask;
            }
            
            item.CategorySet.Add(category);
            category.TodoItemSet.Add(item);
            return Task.CompletedTask;
        }
    }
}