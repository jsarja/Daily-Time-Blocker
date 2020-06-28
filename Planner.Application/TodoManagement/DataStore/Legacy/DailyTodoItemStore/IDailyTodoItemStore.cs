using System.Collections.Generic;
using Planner.Application.TodoManagement.TodoActions.ReadActions;
using Planner.Domain.Entities;

namespace Planner.Application.TodoManagement.DataStore.DailyTodoItemStore
{
    public interface IDailyTodoItemBlockStore
    {
        public void DailyTodoItemDeletion(int id);
        public int DailyTodoItemInsertion(DailyTodoItem item);
        public void DailyTodoItemModification(int id, DailyTodoItemBlock item);
        
        public DailyTodoItem DailyTodoItemQuery(int id);
        public IEnumerable<DailyTodoItem> DailyTodoItemsQuery(GetDailyTodoItemsSearchArgs searchArgs);
    }
}