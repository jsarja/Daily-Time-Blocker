using System;
using System.Collections.Generic;
using System.Linq;
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
            var _ = searchArgs != null ? "" : throw new ArgumentNullException();

            var results = await m_dbContext.TodoItems.Where(i => 
                (searchArgs.IsInUserFavorites == null || i.IsUserFavorite == searchArgs.IsInUserFavorites)
                
                && (searchArgs.StringFieldsContains == null || i.Title.Contains(searchArgs.StringFieldsContains)
                                                            || i.Description.Contains(searchArgs.StringFieldsContains))
            ).ToListAsync();

            if (searchArgs.CategoryId != null)
            {
                results = results.Where(i => 
                    i.CategorySet.Any(c => c.TodoItemCategoryId == searchArgs.CategoryId)).ToList();
            }

            return results;
        }

        public async Task<DailyTodoItem> DailyTodoItemQueryAsync(int id)
        {
            return await m_dbContext.DailyTodoItems.FindAsync(id);
        }

        public async Task<IEnumerable<DailyTodoItem>> DailyTodoItemsQueryAsync(GetDailyTodoItemsSearchArgs searchArgs)
        {
            var _ = searchArgs != null ? "" : throw new ArgumentNullException();
            
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
            var _ = searchArgs != null ? "" : throw new ArgumentNullException();
            var results = await m_dbContext.DailyTodoItemBlocks
                .Where(b =>
                    (searchArgs.TimeIntervalStart == null || searchArgs.TimeIntervalStart <= b.StartTime)

                    && (searchArgs.TimeIntervalEnd == null || searchArgs.TimeIntervalEnd > b.StartTime)

                    && (searchArgs.IsCompleted == null || searchArgs.IsCompleted == b.IsCompleted)
                )
                .ToListAsync();

                if (searchArgs.Date != null)
                {
                    results = results.Where(b => searchArgs.Date.Equals(b.DTodoItem.TodoDate)).ToList();
                }
            
            return results;
        }

        public async Task<TodoItemCategory> TodoItemCategoryQueryAsync(int id)
        {
            return await m_dbContext.TodoItemCategories.FindAsync(id);
        }

        public async Task<IEnumerable<TodoItemCategory>> 
            TodoItemCategoriesQueryAsync(TodoItemCategoriesSearchArgs searchArgs)
        {
            var _ = searchArgs != null ? "" : throw new ArgumentNullException();
            
            var results = await m_dbContext.TodoItemCategories.Where(c => 
                (searchArgs.StringFieldsContains == null || c.Title.Contains(searchArgs.StringFieldsContains)
                                                         || c.Description.Contains(searchArgs.StringFieldsContains))
            ).ToListAsync();

            if (searchArgs.TodoItemId != null)
            {
                results = results.Where(c => c.TodoItemSet.Any(
                    i => searchArgs.TodoItemId == i.TodoItemId)).ToList();
            }
            
            return results;
        }
    }
}