using Planner.Domain.Entities;

namespace Planner.Application.Common.Models
{
    public class TodoItemCategoryJoinTable
    {
        private TodoItem m_todoItem;

        public TodoItem TodoItem
        {
            get => m_todoItem;
            set { 
                m_todoItem = value;
                TodoItemId = value.TodoItemId;
            }
        }

        private int m_itemId;
        public int TodoItemId
        {
            get => m_itemId;
            private set
            {
                if (value == m_todoItem.TodoItemId)
                {
                    m_itemId = value;
                }
            }
        }

        private TodoItemCategory m_category;

        public TodoItemCategory Category
        {
            get => m_category;
            set { 
                m_category = value;
                CategoryId = value.TodoItemCategoryId;
            }
        }
        
        private int m_categoryId;
        public int CategoryId { 
            get => m_categoryId;
            private set
            {
                if (value == m_category.TodoItemCategoryId)
                {
                    m_categoryId = value;
                }
            } 
        }
    }
}