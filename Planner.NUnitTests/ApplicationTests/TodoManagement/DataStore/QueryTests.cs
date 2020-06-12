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
                todo1.IsUserFavorite, todo1.Categories.Count);
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
            // Set multiple search terms.
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
    }
}