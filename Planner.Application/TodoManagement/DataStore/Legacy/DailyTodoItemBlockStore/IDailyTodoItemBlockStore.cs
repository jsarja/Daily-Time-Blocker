using System.Collections.Generic;
using Planner.Application.TodoManagement.TodoActions.ReadActions;
using Planner.Domain.Entities;

namespace Planner.Application.TodoManagement.DataStore.DailyTodoItemBlockStore
{
    public interface IDailyTodoItemStore
    {
        public void DailyTodoItemBlockDeletion(int id);
        public int DailyTodoItemBlockInsertion(DailyTodoItemBlock block);
        public void DailyTodoItemBlockModification(int id, DailyTodoItemBlock block);
        
        public DailyTodoItemBlock DailyTodoItemBlockQuery(int id);
        public IEnumerable<DailyTodoItemBlock> DailyTodoItemBlocksQuery(GetDailyTodoItemBlocksSearchArgs searchArgs);

    }
}