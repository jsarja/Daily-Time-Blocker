using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Planner.Application.TodoManagement.DataStore.DataStoreQuery;
using Planner.Application.TodoManagement.TodoActions.ReadActions;
using Planner.NUnitTests.TestData;

namespace Planner.NUnitTests.ApplicationTests.TodoManagement.DataStore
{
    public abstract class QueryTests
    {
        protected IDataQueryOperations m_queryClient;
        
        [Test]
        public async Task TestTodoItemQueryWithId()
        {
            var todo1 = await m_queryClient.TodoItemQueryAsync(1);
            Assert.That(todo1, Is.Not.Null);

            var propertyValues = (todo1.Title, todo1.OwnerId, todo1.Description,
                todo1.IsUserFavorite, todo1.CategorySet.Count);
            var expectedPropertyValues = ("TodoItem 1", 1, "This a todo item number 1. Hello", true, 1);
            Assert.That(propertyValues, Is.EqualTo(expectedPropertyValues));

            for (var i = 2; i < 5; i++)
            {
                var todo = await m_queryClient.TodoItemQueryAsync(i);
                Assert.That(todo, Is.Not.Null);
            }
        }
        
        [Test]
        public async Task TestTodoItemQueryWithCategoryId()
        {
            var todoItems =
                await m_queryClient.TodoItemsQueryAsync(new TodoItemsSearchArgs {CategoryId = 1});
            Assert.That(todoItems, Has.Count.EqualTo(2));

            todoItems = await m_queryClient.TodoItemsQueryAsync(new TodoItemsSearchArgs {CategoryId = 2});
            Assert.That(todoItems, Has.Count.EqualTo(2));
        }
        
        [Test]
        public async Task TestTodoItemQueryWithTextSearch()
        {
            var todoItems = 
                await m_queryClient.TodoItemsQueryAsync(new TodoItemsSearchArgs{ StringFieldsContains = "This a todo item " });
            Assert.That(todoItems, Has.Count.EqualTo(4));
            
            var singleTodoItem = (await m_queryClient
                .TodoItemsQueryAsync(new TodoItemsSearchArgs{ StringFieldsContains = " 1" }))
                .ToList();
            Assert.That(singleTodoItem, Is.Not.Null);
            Assert.That(singleTodoItem, Has.Count.EqualTo(1));
            Assert.That(singleTodoItem.First().TodoItemId, Is.EqualTo(1));
        }
        
        [Test]
        public async Task TestTodoItemQueryWithUserFavorites()
        {
            var todoItems = 
                await m_queryClient.TodoItemsQueryAsync(new TodoItemsSearchArgs{ IsInUserFavorites = true });
            Assert.That(todoItems, Has.Count.EqualTo(3));
        }
        
        [Test]
        public async Task TestTodoItemQueryWithMultipleArgs()
        {
            var singleTodoItem = (await m_queryClient.TodoItemsQueryAsync(
                new TodoItemsSearchArgs
                {
                    CategoryId = 2,
                    IsInUserFavorites = true
                })).ToList();
            Assert.That(singleTodoItem, Has.Count.EqualTo(1));
            Assert.That(singleTodoItem.First().TodoItemId, Is.EqualTo(5));

            var todoItems = await m_queryClient.TodoItemsQueryAsync(
                new TodoItemsSearchArgs
                {
                    StringFieldsContains = "Hello",
                    IsInUserFavorites = true
                });
            Assert.That(todoItems, Has.Count.EqualTo(2));
        }
        
        [Test]
        public async Task TestTodoItemQueryWithoutArgs()
        {
            var todoItems = await m_queryClient.TodoItemsQueryAsync(new TodoItemsSearchArgs());
            Assert.That(todoItems, Has.Count.EqualTo(5));
        }

        [Test]
        public async Task TestTodoItemQueryWithNoResults()
        {
            var todo1 = await m_queryClient.TodoItemQueryAsync(9);
            Assert.That(todo1, Is.Null);
            
            var todoItems = await m_queryClient.TodoItemsQueryAsync(new TodoItemsSearchArgs {CategoryId = 3});
            Assert.That(todoItems, Has.Count.EqualTo(0));
            
            todoItems = await m_queryClient.TodoItemsQueryAsync(new TodoItemsSearchArgs{ StringFieldsContains = "Not in data" });
            Assert.That(todoItems, Has.Count.EqualTo(0));
            
            todoItems = await m_queryClient.TodoItemsQueryAsync(
                new TodoItemsSearchArgs
                {
                    CategoryId = 2,
                    IsInUserFavorites = false,
                    StringFieldsContains = "5"
                });
            Assert.That(todoItems, Has.Count.EqualTo(0));
        }
        
        [Test]
        public void TestTodoItemQueryWithNullArgs()
        {
            Assert.That(async () => await m_queryClient.TodoItemsQueryAsync(null),
                Throws.ArgumentNullException);
        }
        
        [Test]
        public async Task TestDailyTodoItemQueryWithId()
        {
            var dTodo1 = await m_queryClient.DailyTodoItemQueryAsync(1);
            Assert.That(dTodo1, Is.Not.Null);

            var propertyValues = (dTodo1.TodoDate, dTodo1.TimeUsedForTodo, 
                dTodo1.TimeReservedForTodo);
            var expectedPropertyValues = (new DateTime(2020, 6, 9),
                new TimeSpan(0, 45, 0), new TimeSpan(0, 45, 0));
            Assert.That(propertyValues, Is.EqualTo(expectedPropertyValues));

            for (var i = 2; i < 7; i++)
            {
                var dTodo = await m_queryClient.DailyTodoItemQueryAsync(i);
                Assert.That(dTodo, Is.Not.Null);
            }
        }
        
        [Test]
        public async Task TestDailyTodoItemQueryWithDate()
        {
            var dTodoItems =
                await m_queryClient.DailyTodoItemsQueryAsync(new GetDailyTodoItemsSearchArgs
                {
                    Date = new DateTime(2020, 6, 9)
                });
            Assert.That(dTodoItems, Has.Count.EqualTo(4));
        }

        [Test]
        public async Task TestDailyTodoItemQueryWithoutArgs()
        {
            var dTodoItems = await m_queryClient.DailyTodoItemsQueryAsync(
                new GetDailyTodoItemsSearchArgs());
            Assert.That(dTodoItems, Has.Count.EqualTo(6));
        }

        [Test]
        public async Task TestDailyTodoItemQueryWithNoResults()
        {
            var dTodo1 = await m_queryClient.DailyTodoItemQueryAsync(9);
            Assert.That(dTodo1, Is.Null);
            
            var todoItems = await m_queryClient.DailyTodoItemsQueryAsync(
                new GetDailyTodoItemsSearchArgs {Date = new DateTime(2020, 6, 30)});
            Assert.That(todoItems, Has.Count.EqualTo(0));
        }
        
        [Test]
        public void TestDailyTodoItemQueryWithNullArgs()
        {
            Assert.That(async () => await m_queryClient.DailyTodoItemsQueryAsync(null),
                Throws.ArgumentNullException);
        }
    }
}