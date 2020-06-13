using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Planner.Application.TodoManagement.DataStore.Core;
using Planner.Application.TodoManagement.TodoActions.ReadActions;
using Planner.Domain.Entities;

namespace Planner.Application.TodoManagement.DataStore.DataStoreQuery
{
    public class InMemoryDataQuery : IDataQueryOperations
    {
        private readonly MockDataRepo m_dataRepo;
        public InMemoryDataQuery(MockDataRepo dataRepo)
        {
            m_dataRepo = dataRepo;
        }

        public Task<bool> CommitAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }

        public Task<TodoItem> TodoItemQueryAsync(int id)
        {
            var item = m_dataRepo.TodoItems.Find(i => i.TodoItemId == id);
            return Task.FromResult(item);
        }

        public Task<IEnumerable<TodoItem>> TodoItemsQueryAsync(TodoItemsSearchArgs searchArgs)
        {
            var _ = searchArgs != null ? "" : throw new ArgumentNullException();
            
            var items = m_dataRepo.TodoItems.FindAll(i => 
                (searchArgs.CategoryId == null 
                    || i.CategorySet.Any(c => searchArgs.CategoryId == c.TodoItemCategoryId))
                
                && (searchArgs.IsInUserFavorites == null || i.IsUserFavorite == searchArgs.IsInUserFavorites)
                
                && (searchArgs.StringFieldsContains == null || i.Title.Contains(searchArgs.StringFieldsContains)
                                                            || i.Description.Contains(searchArgs.StringFieldsContains))
            );

            return Task.FromResult((IEnumerable<TodoItem>)items);
        }

        public Task<DailyTodoItem> DailyTodoItemQueryAsync(int id)
        {
            var item = m_dataRepo.DailyTodoItems.Find(i => i.DailyTodoItemId == id);
            return Task.FromResult(item);
        }

        public Task<IEnumerable<DailyTodoItem>> DailyTodoItemsQueryAsync(GetDailyTodoItemsSearchArgs searchArgs)
        {
            var _ = searchArgs != null ? "" : throw new ArgumentNullException();
            
            var items = m_dataRepo.DailyTodoItems.FindAll(i => 
                (searchArgs.Date == null || searchArgs.Date.Equals(i.TodoDate)));

            return Task.FromResult((IEnumerable<DailyTodoItem>)items);
        }

        public Task<DailyTodoItemBlock> DailyTodoItemBlockQueryAsync(int id)
        {
            var item = m_dataRepo.DailyTodoItemBlocks.Find(i => i.DailyTodoItemBlockId == id);
            return Task.FromResult(item);
        }

        public Task<IEnumerable<DailyTodoItemBlock>> DailyTodoItemBlocksQueryAsync(GetDailyTodoItemBlocksSearchArgs searchArgs)
        {
            var _ = searchArgs != null ? "" : throw new ArgumentNullException();
            
            var blocks = m_dataRepo.DailyTodoItemBlocks.FindAll(b => 
                (searchArgs.TimeIntervalStart == null || searchArgs.TimeIntervalStart == b.StartTime)
                
                && (searchArgs.TimeIntervalEnd == null || searchArgs.TimeIntervalEnd == b.EndTime)
                
                && (searchArgs.IsCompleted == null || searchArgs.IsCompleted == b.IsCompleted)
            );

            return Task.FromResult((IEnumerable<DailyTodoItemBlock>)blocks);
        }

        public Task<TodoItemCategory> TodoItemCategoryQueryAsync(int id)
        {
            var item = m_dataRepo.TodoItemCategories.Find(i => i.TodoItemCategoryId == id);
            return Task.FromResult(item);
        }

        public Task<IEnumerable<TodoItemCategory>> TodoItemCategoriesQueryAsync(TodoItemCategoriesSearchArgs searchArgs)
        {
            var _ = searchArgs != null ? "" : throw new ArgumentNullException();
            
            var categories = m_dataRepo.TodoItemCategories.FindAll(c => 
                (searchArgs.TodoItemId == null 
                    || c.TodoItemSet.Any(i => searchArgs.TodoItemId == i.TodoItemId))
                
                && (searchArgs.StringFieldsContains == null || c.Title.Contains(searchArgs.StringFieldsContains)
                                                            || c.Description.Contains(searchArgs.StringFieldsContains))
            );

            return Task.FromResult((IEnumerable<TodoItemCategory>)categories);
        }
    }
}