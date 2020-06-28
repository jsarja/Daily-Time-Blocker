using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Planner.Application.Common.Interfaces;
using Planner.Application.TodoManagement.DataStore.Core;

namespace Planner.Application.TodoManagement.DataStore.DataStoreDeletion
{
    public class DbContextDataDeletion : DbContextOperationsBase, IDataDeletionOperations
    {
        public DbContextDataDeletion(IApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task TodoItemDeletionAsync(int id)
        {
            var todoItem = await m_dbContext.TodoItems.FindAsync(id);
            
            if (todoItem != null)
            {
                m_dbContext.TodoItems.Remove(todoItem);
            }

            await m_dbContext.TodoItemCategories.ForEachAsync(t => t.TodoItemSet.Remove(todoItem));
        }

        public async Task DailyTodoItemDeletionAsync(int id)
        {
            var dTodoItem= await m_dbContext.DailyTodoItems.FindAsync(id);
            
            if (dTodoItem != null)
            {
                m_dbContext.DailyTodoItems.Remove(dTodoItem);
            }
        }

        public async Task DailyTodoItemBlockDeletionAsync(int id)
        {
            var block= await m_dbContext.DailyTodoItemBlocks.FindAsync(id);
            
            if (block != null)
            {
                m_dbContext.DailyTodoItemBlocks.Remove(block);
            }
        }

        public async Task TodoItemCategoryDeletionAsync(int id)
        {
            var category= await m_dbContext.TodoItemCategories.FindAsync(id);
            
            if (category != null)
            {
                m_dbContext.TodoItemCategories.Remove(category);
            }

            await m_dbContext.TodoItems.ForEachAsync(t => t.CategorySet.Remove(category));
        }
    }
}