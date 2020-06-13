using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Planner.Application.TodoManagement.DataStore.Core;

namespace Planner.Application.TodoManagement.DataStore.DataStoreDeletion
{
    public class InMemoryDataDeletion : IDataDeletionOperations
    {
        private readonly MockDataRepo m_dataRepo;
        public InMemoryDataDeletion(MockDataRepo dataRepo)
        {
            m_dataRepo = dataRepo;
        }
        
        public Task TodoItemDeletionAsync(int id)
        {
            var todoItem = m_dataRepo.TodoItems.Find(c => c.TodoItemId == id);
            
            if (todoItem == null)
            {
                return Task.CompletedTask;
            }
            
            m_dataRepo.TodoItemCategories.ForEach(c =>  c.TodoItemSet.Remove(todoItem));
            m_dataRepo.TodoItems.Remove(todoItem);
            return Task.CompletedTask;
        }

        public Task DailyTodoItemDeletionAsync(int id)
        {
            m_dataRepo.DailyTodoItems.RemoveAll(i => i.DailyTodoItemId == id);
            return Task.CompletedTask;
        }

        public Task DailyTodoItemBlockDeletionAsync(int id)
        {
            m_dataRepo.DailyTodoItemBlocks.RemoveAll(i => i.DailyTodoItemBlockId == id);
            return Task.CompletedTask;
        }

        public Task TodoItemCategoryDeletionAsync(int id)
        {
            var todoItemCategory = m_dataRepo.TodoItemCategories
                .Find(c => c.TodoItemCategoryId == id);
            
            if (todoItemCategory == null)
            {
                return Task.CompletedTask;
            }
           
            m_dataRepo.TodoItems.ForEach(i =>  i.CategorySet.Remove(todoItemCategory));
            m_dataRepo.TodoItemCategories.Remove(todoItemCategory);

            return Task.CompletedTask;
        }

        public Task<bool> CommitAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}