using System;
using System.Collections.Generic;
using Planner.Domain.Entities;

namespace Planner.Application.TodoManagement.DataStore.Core
{
    public class MockDataRepo
    {
        public readonly List<TodoItem> TodoItems = new List<TodoItem>();

        public readonly List<DailyTodoItem> DailyTodoItems = new List<DailyTodoItem>();

        public readonly List<DailyTodoItemBlock> DailyTodoItemBlocks = new List<DailyTodoItemBlock>();
        
        public readonly List<TodoItemCategory> TodoItemCategories = new List<TodoItemCategory>();

        public MockDataRepo(bool populate = true)
        {
            if (!populate)
            {
                return;
            }
            
            InitTodoItems();
            InitDailyTodoItems();
            InitDailyTodoItemsBlocks();
            InitTodoItemCategories();
        }

        private void InitTodoItems()
        {
            TodoItems.Add(new TodoItem
            {
                TodoItemId = 1,
                Title = "TodoItem 1",
                OwnerId = 1,
                Description = "This a todo item number 1. Hello",
                IsUserFavorite = true
            });
            TodoItems.Add(new TodoItem
            {
                TodoItemId = 2,
                Title = "TodoItem 2",
                OwnerId = 1,
                Description = "This a todo item number 2. Hello",
                IsUserFavorite = false
            });
            TodoItems.Add(new TodoItem
            {
                TodoItemId = 3,
                Title = "TodoItem 3",
                OwnerId = 1,
                Description = "This a todo item number 3. Hello",
                IsUserFavorite = true
            });
            TodoItems.Add(new TodoItem
            {
                TodoItemId = 4,
                Title = "TodoItem 4",
                OwnerId = 1,
                Description = "This a todo item number 4.",
                IsUserFavorite = false
            });
            TodoItems.Add(new TodoItem
            {
                TodoItemId = 5,
                Title = "TodoItem #",
                OwnerId = 1,
                Description = "Number 5 todo Item.",
                IsUserFavorite = true
            });
        }

        private void InitDailyTodoItems()
        {
            DailyTodoItems.Add( new DailyTodoItem
            {
                DailyTodoItemId = 1,
                TodoInfo = TodoItems[0],
                TodoDate = new DateTime(2020, 6, 9),
                TimeUsedForTodo = new TimeSpan(0, 45, 0),
                TimeReservedForTodo = new TimeSpan(0, 45, 0)
            });
            DailyTodoItems.Add(new DailyTodoItem
            {
                DailyTodoItemId = 2,
                TodoInfo = TodoItems[0],
                TodoDate = new DateTime(2020, 6, 10),
                TimeUsedForTodo = new TimeSpan(0),
                TimeReservedForTodo = new TimeSpan(2, 0, 0),
            });
            DailyTodoItems.Add(new DailyTodoItem
            {
                DailyTodoItemId = 3,
                TodoInfo = TodoItems[1],
                TodoDate = new DateTime(2020, 6, 9),
                TimeUsedForTodo = new TimeSpan(1, 0, 0),
                TimeReservedForTodo = new TimeSpan(2, 0, 0)
            });
            DailyTodoItems.Add(new DailyTodoItem
            {
                DailyTodoItemId = 4,
                TodoInfo = TodoItems[2],
                TodoDate = new DateTime(2020, 6, 9),
                TimeUsedForTodo = new TimeSpan(0)
            });
            DailyTodoItems.Add(new DailyTodoItem
            {
                DailyTodoItemId = 5,
                TodoInfo = TodoItems[3],
                TodoDate = new DateTime(2020, 6, 9),
                TimeUsedForTodo = new TimeSpan(0, 30, 0)
            });
            DailyTodoItems.Add(new DailyTodoItem
            {
                DailyTodoItemId = 6,
                TodoInfo = TodoItems[3],
                TodoDate = new DateTime(2020, 6, 10),
                TimeUsedForTodo = new TimeSpan(0),
                TimeReservedForTodo = new TimeSpan(3, 0, 0)
            });
        }

        private void InitDailyTodoItemsBlocks()
        {
            DailyTodoItemBlocks.Add(new DailyTodoItemBlock
            {
                DailyTodoItemBlockId = 1,
                DTodoItem = DailyTodoItems[0],
                StartTime = new DateTime(2020, 6, 9, 9, 0, 0),
                EndTime = new DateTime(2020, 6, 9, 9, 45, 0),
                IsCompleted = true
            });

            DailyTodoItemBlocks.Add(new DailyTodoItemBlock
            {
                DailyTodoItemBlockId = 2,
                DTodoItem = DailyTodoItems[2],
                StartTime = new DateTime(2020, 6, 9, 10, 0, 0),
                EndTime = new DateTime(2020, 6, 9, 11, 0, 0),
                IsCompleted = true
            });
            
            DailyTodoItemBlocks.Add(new DailyTodoItemBlock
            {
                DailyTodoItemBlockId = 3,
                DTodoItem = DailyTodoItems[2],
                StartTime = new DateTime(2020, 6, 9, 13, 0, 0),
                EndTime = new DateTime(2020, 6, 9, 14, 0, 0),
                IsCompleted = false
            });
            
            DailyTodoItemBlocks.Add(new DailyTodoItemBlock
            {
                DailyTodoItemBlockId = 4,
                DTodoItem = DailyTodoItems[3],
                StartTime = new DateTime(2020, 6, 9, 14, 0, 0),
                EndTime = new DateTime(2020, 6, 9, 15, 0, 0),
                IsCompleted = false
            });
            
            DailyTodoItemBlocks.Add(new DailyTodoItemBlock
            {
                DailyTodoItemBlockId = 5,
                DTodoItem = DailyTodoItems[4],
                StartTime = new DateTime(2020, 6, 9, 11, 15, 0),
                EndTime = new DateTime(2020, 6, 9, 11, 45, 0),
                IsCompleted = true
            });
            
            DailyTodoItemBlocks.Add(new DailyTodoItemBlock
            {
                DailyTodoItemBlockId = 6,
                DTodoItem = DailyTodoItems[5],
                StartTime = new DateTime(2020, 6, 10, 12, 0, 0),
                EndTime = new DateTime(2020, 6, 10, 13, 0, 0),
                IsCompleted = false
            });
            
            DailyTodoItemBlocks.Add(new DailyTodoItemBlock
            {
                DailyTodoItemBlockId = 7,
                DTodoItem = DailyTodoItems[5],
                StartTime = new DateTime(2020, 6, 10, 15, 0, 0),
                EndTime = new DateTime(2020, 6, 10, 16, 0, 0),
                IsCompleted = false
            });
        }

        private void InitTodoItemCategories()
        {
            TodoItemCategories.Add(new TodoItemCategory
            {
                TodoItemCategoryId = 1,
                Title = "Category 1",
                Description = "This a category number 1.",
                OwnerId = 1
            });
            TodoItemCategories.Add(new TodoItemCategory
            {
                TodoItemCategoryId = 2,
                Title = "Category 2",
                Description = "This a category number 2.",
                OwnerId = 1
            });
            
            TodoItemCategories[0].TodoItemSet.Add(TodoItems[0]);
            TodoItemCategories[0].TodoItemSet.Add(TodoItems[2]);
            TodoItems[0].CategorySet.Add(TodoItemCategories[0]);
            TodoItems[2].CategorySet.Add(TodoItemCategories[0]);
            
            TodoItemCategories[1].TodoItemSet.Add(TodoItems[3]);
            TodoItemCategories[1].TodoItemSet.Add(TodoItems[4]);
            TodoItems[3].CategorySet.Add(TodoItemCategories[1]);
            TodoItems[4].CategorySet.Add(TodoItemCategories[1]);
        }
    }
}