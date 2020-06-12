using System.Collections.Generic;
using Planner.Application.Common.Interfaces;
using Planner.Application.TodoManagement.Dtos;

namespace Planner.Application.TodoManagement.TodoActions.ReadActions
{
    public class ReadActionsPersistent : IReadActions
    {
        private readonly IApplicationDbContext m_dataAccessClient;
        
        public ReadActionsPersistent(IApplicationDbContext dataAccessClient)
        {
            m_dataAccessClient = dataAccessClient;
        }
        
        public TodoItemDto GetTodoItem(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TodoItemDto> GetTodoItems()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TodoItemDto> GetTodoItems(TodoItemsSearchArgs searchArgs)
        {
            throw new System.NotImplementedException();
        }

        public DailyTodoItemDto GetDailyTodoItem(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<DailyTodoItemDto> GetDailyTodoItems()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<DailyTodoItemDto> GetDailyTodoItems(GetDailyTodoItemsSearchArgs searchArgs)
        {
            throw new System.NotImplementedException();
        }

        public DailyTodoItemBlockDto GetDailyTodoItemBlock(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<DailyTodoItemBlockDto> GetDailyTodoItemBlocks()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<DailyTodoItemBlockDto> GetDailyTodoItemBlocks(GetDailyTodoItemBlocksSearchArgs searchArgs)
        {
            throw new System.NotImplementedException();
        }

        public TodoItemCategoryDto GetTodoItemCategory(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TodoItemCategoryDto> GetTodoItemCategories()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TodoItemCategoryDto> GetTodoItemCategories(TodoItemCategoriesSearchArgs searchArgs)
        {
            throw new System.NotImplementedException();
        }
    }
}