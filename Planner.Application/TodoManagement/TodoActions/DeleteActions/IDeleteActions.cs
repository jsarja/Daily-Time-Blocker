namespace Planner.Application.TodoManagement.TodoActions.DeleteActions
{
    public interface IDeleteActions
    {
        public void DeleteTodoItem(int id);
        public void DeleteDailyTodoItem(int id);
        public void DeleteDailyTodoItemBlock(int id);
        public void DeleteTodoItemCategory(int id);
    }
}