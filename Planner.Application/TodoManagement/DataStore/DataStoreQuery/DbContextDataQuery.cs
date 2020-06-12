using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Planner.Application.Common.Interfaces;
using Planner.Application.TodoManagement.TodoActions.ReadActions;
using Planner.Application.TodoManagement.DataStore.Core;
using Planner.Domain.Entities;

namespace Planner.Application.TodoManagement.DataStore.DataStoreQuery
{
    public class DbContextDataQuery : DbContextOperationsBase, IDataQueryOperations
    {
        public DbContextDataQuery(IApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<TodoItem> TodoItemQueryAsync(int id)
        {
            return await m_dbContext.TodoItems.FindAsync(id);
        }

        public async Task<IEnumerable<TodoItem>> TodoItemsQueryAsync(TodoItemsSearchArgs searchArgs)
        {

            var results = await m_dbContext.TodoItems.Where(i =>
                (searchArgs.CategoryId == null || m_dbContext.TodoItemCategoryJoin
                     .Any(j => j.CategoryId == searchArgs.CategoryId 
                               && j.TodoItemId == i.TodoItemId))

                && (searchArgs.IsInUserFavorites == null || i.IsUserFavorite == searchArgs.IsInUserFavorites)
                
                && (searchArgs.StringFieldsContains == null || i.Title.Contains(searchArgs.StringFieldsContains)
                                                            || i.Description.Contains(searchArgs.StringFieldsContains))
            ).ToListAsync();
            
            return results;
        }

        public async Task<DailyTodoItem> DailyTodoItemQueryAsync(int id)
        {
            return await m_dbContext.DailyTodoItems.FindAsync(id);
        }

        public async Task<IEnumerable<DailyTodoItem>> DailyTodoItemsQueryAsync(GetDailyTodoItemsSearchArgs searchArgs)
        {
            var results = await m_dbContext.DailyTodoItems.Where(i =>
                searchArgs.Date == null || searchArgs.Date.Equals(i.TodoDate)).ToListAsync();

            return results;
        }

        public async Task<DailyTodoItemBlock> DailyTodoItemBlockQueryAsync(int id)
        {
            return await m_dbContext.DailyTodoItemBlocks.FindAsync(id);
        }

        public async Task<IEnumerable<DailyTodoItemBlock>> DailyTodoItemBlocksQueryAsync(GetDailyTodoItemBlocksSearchArgs searchArgs)
        {
            var results = await m_dbContext.DailyTodoItemBlocks.Where(b => 
                (searchArgs.TimeIntervalStart == null || searchArgs.TimeIntervalStart == b.StartTime)
                
                && (searchArgs.TimeIntervalEnd == null || searchArgs.TimeIntervalEnd == b.EndTime)
                
                && (searchArgs.IsCompleted == null || searchArgs.IsCompleted == b.IsCompleted)
            ).ToListAsync();

            return results;
        }

        public async Task<TodoItemCategory> TodoItemCategoryQueryAsync(int id)
        {
            var category = await m_dbContext.TodoItemCategories.FindAsync(id);
            
            // var itemIdList = await m_dbContext.TodoItemCategoryJoin
            //     .Where(j => j.CategoryId == id)
            //     .Select(j => j.TodoItemId)
            //     .ToListAsync();

            // var items = await m_dbContext.TodoItems.Where(i =>
            //     itemIdList.Contains(i.TodoItemId)).ToListAsync();
            //
            // foreach (var item in items)
            // {
            //     category.TodoCollection.Add(item);
            // }
            
            return category;
        }

        public async Task<IEnumerable<TodoItemCategory>> 
            TodoItemCategoriesQueryAsync(TodoItemCategoriesSearchArgs searchArgs)
        {
            var results = await m_dbContext.TodoItemCategories.Where(c => 
                (searchArgs.TodoItemId == null 
                 || c.TodoCollection.Any(i => searchArgs.TodoItemId == i.TodoItemId))
                
                && (searchArgs.StringFieldsContains == null || c.Title.Contains(searchArgs.StringFieldsContains)
                                                            || c.Description.Contains(searchArgs.StringFieldsContains))
            ).ToListAsync();

            return results;
        }
    }
}