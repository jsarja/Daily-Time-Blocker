using System.Threading.Tasks;
using Planner.Application.Common.Interfaces;
using Planner.Application.Common.Models;
using Planner.Application.TodoManagement.DataStore.Core;
using Planner.Domain.Entities;

namespace Planner.Application.TodoManagement.DataStore.DataStoreInsertion
{
    public class DbContextDataInsertion : DbContextOperationsBase, IDataInsertionOperations
    {
        public DbContextDataInsertion(IApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<int> TodoItemInsertionAsync(TodoItem item)
        {
            await m_dbContext.TodoItems.AddAsync(item);
            return item.TodoItemId;
        }

        public async Task<int>  DailyTodoItemInsertionAsync(DailyTodoItem item)
        {
            await m_dbContext.DailyTodoItems.AddAsync(item);
            return item.DailyTodoItemId;
        }

        public async Task<int>  DailyTodoItemBlockInsertionAsync(DailyTodoItemBlock block)
        {
            await m_dbContext.DailyTodoItemBlocks.AddAsync(block);
            return block.DailyTodoItemBlockId;
        }

        public async Task<int>  TodoItemCategoryInsertionAsync(TodoItemCategory category)
        {
            await m_dbContext.TodoItemCategories.AddAsync(category);
            return category.TodoItemCategoryId;
        }

        public async Task TodoItemToCategoryInsertionAsync(int categoryId, int itemId)
        {
            var item = await m_dbContext.TodoItems.FindAsync(itemId);
            var category = await m_dbContext.TodoItemCategories.FindAsync(categoryId);

            if (item == null || category == null)
            {
                return;
            }
            
            var join = new TodoItemCategoryJoinTable
            {
                TodoItem = item,
                Category = category,
            };
            
            await m_dbContext.TodoItemCategoryJoin.AddAsync(join);
        }
    }
}