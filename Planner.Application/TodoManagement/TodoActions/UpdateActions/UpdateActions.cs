using Planner.Application.Common.Interfaces;
using Planner.Application.TodoManagement.Dtos;

namespace Planner.Application.TodoManagement.TodoActions.UpdateActions
{
    public class UpdateActions : IUpdateActions
    {
        private readonly IApplicationDbContext m_dataAccessClient;
        
        public UpdateActions(IApplicationDbContext dataAccessClient)
        {
            m_dataAccessClient = dataAccessClient;
        }

        public TodoItemDto UpdateTodoItem(UpdateTodoItemArgs item)
        {
            throw new System.NotImplementedException();
        }

        public DailyTodoItemDto UpdateDailyTodoItem(UpdateDailyTodoItemArgs item)
        {
            throw new System.NotImplementedException();
        }

        public DailyTodoItemBlockDto UpdateDailyTodoItemBlock(UpdateDailyTodoItemBlockArgs item)
        {
            throw new System.NotImplementedException();
        }

        public TodoItemCategoryDto UpdateTodoItemCategory(UpdateTodoItemCategoryArgs item)
        {
            throw new System.NotImplementedException();throw new System.NotImplementedException();
        }

        public TodoItemCategoryDto AddTodoItemToCategory(int categoryId, int itemId)
        {
            throw new System.NotImplementedException();
        }
    }
}