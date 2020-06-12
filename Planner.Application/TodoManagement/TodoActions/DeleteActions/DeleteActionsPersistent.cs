using Planner.Application.Common.Interfaces;

namespace Planner.Application.TodoManagement.TodoActions.DeleteActions
{
    public class DeleteActionsPersistent : IDeleteActions
    {
        private readonly IApplicationDbContext m_dataAccessClient;
        
        public DeleteActionsPersistent(IApplicationDbContext dataAccessClient)
        {
            m_dataAccessClient = dataAccessClient;
        }

        public void DeleteTodoItem(int id)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteDailyTodoItem(int id)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteDailyTodoItemBlock(int id)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteTodoItemCategory(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}