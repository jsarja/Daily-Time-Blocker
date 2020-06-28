using System;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Planner.Application.Common.Interfaces;
using Planner.Application.TodoManagement.DataStore.Core;
using Planner.Application.Utils;
using Planner.Domain.Entities;

namespace Planner.Application.TodoManagement.DataStore.DataStoreModification
{
    public class DbContextDataModification : DbContextOperationsBase, IDataModificationOperations
    {
        public DbContextDataModification(IApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task TodoItemModificationAsync(int id, TodoItem item)
        {
            var _ = item != null ? "" : throw new ArgumentNullException();
            
            var originalItem = await m_dbContext.TodoItems.FindAsync(id);

            if (originalItem == null)
            {
                return;
            }
            
            originalItem.Title = item.Title.IsNullOrEmpty() ? originalItem.Title : item.Title;
            originalItem.Description = item.Description.IsNullOrEmpty() ? originalItem.Description : item.Description;
            originalItem.IsUserFavorite = item.IsUserFavorite;
        }

        public async Task DailyTodoItemModificationAsync(int id, DailyTodoItem item)
        {
            var _ = item != null ? "" : throw new ArgumentNullException();
            
            var originalItem = await m_dbContext.DailyTodoItems.FindAsync(id);

            if (originalItem == null)
            {
                return;
            }

            originalItem.TodoDate = item.TodoDate != default ? item.TodoDate : originalItem.TodoDate;
            originalItem.TimeUsedForTodo = item.TimeUsedForTodo != default ? item.TimeUsedForTodo 
                : originalItem.TimeUsedForTodo;
            originalItem.TimeReservedForTodo = item.TimeReservedForTodo;
        }

        public async Task DailyTodoItemBlockModificationAsync(int id, DailyTodoItemBlock block)
        {
            var _ = block != null ? "" : throw new ArgumentNullException();
            
            var originalBlock = await m_dbContext.DailyTodoItemBlocks.FindAsync(id);

            if (originalBlock == null)
            {
                return;
            }
            
            originalBlock.StartTime = block.StartTime != default ? block.StartTime : originalBlock.StartTime;
            originalBlock.EndTime = block.EndTime != default ? block.EndTime : originalBlock.EndTime;
            originalBlock.IsCompleted = block.IsCompleted;
        }

        public async Task TodoItemCategoryModificationAsync(int id, TodoItemCategory category)
        {
            var _ = category != null ? "" : throw new ArgumentNullException();
            
            var originalCategory = await m_dbContext.TodoItemCategories.FindAsync(id);

            if (originalCategory == null)
            {
                return;
            }
            
            originalCategory.Title = category.Title.IsNullOrEmpty() ? originalCategory.Title : category.Title;
            originalCategory.Description = category.Description.IsNullOrEmpty() ? originalCategory.Description 
                : category.Description;
        }
    }
}