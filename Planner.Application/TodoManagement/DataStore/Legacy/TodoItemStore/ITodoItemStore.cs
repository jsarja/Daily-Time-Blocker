using System.Collections.Generic;
using Planner.Application.TodoManagement.TodoActions.ReadActions;
using Planner.Domain.Entities;

namespace Planner.Application.TodoManagement.DataStore.TodoItemStore
{
    public interface ITodoItemStore
    {
        public void TodoItemDeletion(int id);
        public int TodoItemInsertion(TodoItem item);
        public void TodoItemModification(int id, TodoItem item);
        
        public TodoItem TodoItemQuery(int id);
        public IEnumerable<TodoItem> TodoItemsQuery(TodoItemsSearchArgs searchArgs);
    }
}