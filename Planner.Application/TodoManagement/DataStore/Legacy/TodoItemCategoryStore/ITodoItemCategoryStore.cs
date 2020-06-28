using System.Collections.Generic;
using Planner.Application.TodoManagement.TodoActions.ReadActions;
using Planner.Domain.Entities;

namespace Planner.Application.TodoManagement.DataStore.TodoItemCategoryStore
{
    public interface ITodoItemCategoryStore
    {
        public void TodoItemCategoryDeletion(int id);
        public int TodoItemCategoryInsertion(TodoItemCategory category);
        public void TodoItemToCategoryInsertion(int categoryId, int itemId);
        public void TodoItemCategoryModification(int id, TodoItemCategory category);
        
        public TodoItemCategory TodoItemCategoryQuery(int id);
        public IEnumerable<TodoItemCategory> TodoItemCategoriesQuery(TodoItemCategoriesSearchArgs searchArgs);
    }
}