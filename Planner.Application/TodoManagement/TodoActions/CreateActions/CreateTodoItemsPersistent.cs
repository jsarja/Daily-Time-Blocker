using Planner.Application.Common.Interfaces;
using Planner.Application.TodoManagement.Dtos;

namespace Planner.Application.TodoManagement.TodoActions.CreateActions
{

    public class CreateTodoItemsPersistent : ICreateTodoItems
    {
        private readonly IApplicationDbContext m_dataAccessClient;
        
        public CreateTodoItemsPersistent(IApplicationDbContext dataAccessClient)
        {
            m_dataAccessClient = dataAccessClient;
        }

        public TodoItemDto CreateTodoItem(CreateTodoItemArgs item)
        {
            throw new System.NotImplementedException();
        }

        public DailyTodoItemDto CreateDailyTodoItem(CreateDailyTodoItemArgs item)
        {
            throw new System.NotImplementedException();
        }

        public DailyTodoItemBlockDto CreateDailyTodoItemBlock(CreateDailyTodoItemBlockArgs item)
        {
            throw new System.NotImplementedException();
        }

        public DailyTodoItemBlockDto CreateDailyTodoItemBlockFromScratch(CreateTodoItemArgs itemData,
            CreateDailyTodoItemArgs dailyItemData, CreateDailyTodoItemBlockArgs blockData)
        {
            throw new System.NotImplementedException();
        }

        public TodoItemCategoryDto CreateTodoItemCategory(CreateTodoItemCategoryArgs item)
        {
            throw new System.NotImplementedException();
        }
    }
}