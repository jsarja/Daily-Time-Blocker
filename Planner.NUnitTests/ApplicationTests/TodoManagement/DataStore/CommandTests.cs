using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Planner.Application.TodoManagement.DataStore.DataStoreDeletion;
using Planner.Application.TodoManagement.DataStore.DataStoreInsertion;
using Planner.Application.TodoManagement.DataStore.DataStoreModification;
using Planner.Application.TodoManagement.DataStore.DataStoreQuery;
using Planner.NUnitTests.TestData;

namespace Planner.NUnitTests.ApplicationTests.TodoManagement.DataStore
{
    public abstract class CommandTests
    {
        protected IDataDeletionOperations m_deletionClient;
        protected IDataInsertionOperations m_insertionClient;
        protected IDataModificationOperations m_modificationClient;
        protected IDataQueryOperations m_queryClient;
        
        [Order(1)]
        [Test]
        public async Task TestTodoItemInsertion()
        {
            var todoItem1 = BasicTodoData.TodoItems[0];
            todoItem1.CategorySet.Clear();
            await m_insertionClient.TodoItemInsertionAsync(todoItem1);
            var success = await m_insertionClient.CommitAsync(new CancellationToken());
            
            Assert.That(success, Is.True);
            var result1 = await m_queryClient.TodoItemQueryAsync(1);
            Assert.That(result1, Is.Not.Null);
            
            var todoItem2 = BasicTodoData.TodoItems[1];
            todoItem2.CategorySet.Clear();
            await m_insertionClient.TodoItemInsertionAsync(todoItem2);
            success = await m_insertionClient.CommitAsync(new CancellationToken());
            
            Assert.That(success, Is.True);
            var result2 = await m_queryClient.TodoItemQueryAsync(2);
            Assert.That(result2, Is.Not.Null);
        }
        
        [Order(2)]
        [Test]
        public async Task TestDailyTodoItemInsertion()
        {
            var dTodoItem1 = BasicTodoData.DailyTodoItems[0];
            await m_insertionClient.DailyTodoItemInsertionAsync(dTodoItem1);
            var success = await m_insertionClient.CommitAsync(new CancellationToken());
            
            Assert.That(success, Is.True);
            var result1 = await m_queryClient.DailyTodoItemQueryAsync(1);
            Assert.That(result1, Is.Not.Null);
            
            var dTodoItem2 = BasicTodoData.DailyTodoItems[1];
            await m_insertionClient.DailyTodoItemInsertionAsync(dTodoItem2);
            success = await m_insertionClient.CommitAsync(new CancellationToken());
            
            Assert.That(success, Is.True);
            var result2 = await m_queryClient.DailyTodoItemQueryAsync(2);
            Assert.That(result2, Is.Not.Null);
            
        }
        
        [Order(3)]
        [Test]
        public async Task TestDailyTodoItemBlockInsertion()
        {
            var dTodoItemBlock1 = BasicTodoData.DailyTodoItemBlocks[0];
            await m_insertionClient.DailyTodoItemBlockInsertionAsync(dTodoItemBlock1);
            var success = await m_insertionClient.CommitAsync(new CancellationToken());
            
            Assert.That(success, Is.True);
            var result1 = await m_queryClient.DailyTodoItemBlockQueryAsync(1);
            Assert.That(result1, Is.Not.Null);
            
            var dTodoItemBlock2 = BasicTodoData.DailyTodoItemBlocks[2];
            await m_insertionClient.DailyTodoItemBlockInsertionAsync(dTodoItemBlock2);
            success = await m_insertionClient.CommitAsync(new CancellationToken());
            
            Assert.That(success, Is.True);
            var result2 = await m_queryClient.DailyTodoItemBlockQueryAsync(3);
            Assert.That(result2, Is.Not.Null);
        }
        
        [Order(4)]
        [Test]
        public async Task TestTodoItemCategoryInsertion()
        {
            var todoItemCategory1 = BasicTodoData.TodoItemCategories[0];
            todoItemCategory1.TodoItemSet.Clear();
            await m_insertionClient.TodoItemCategoryInsertionAsync(todoItemCategory1);
            var success = await m_insertionClient.CommitAsync(new CancellationToken());
            
            Assert.That(success, Is.True);
            var result1 = await m_queryClient.TodoItemCategoryQueryAsync(1);
            Assert.That(result1, Is.Not.Null);
            
            var todoItemCategory2 = BasicTodoData.TodoItemCategories[1];
            todoItemCategory2.TodoItemSet.Clear();
            await m_insertionClient.TodoItemCategoryInsertionAsync(todoItemCategory2);
            success = await m_insertionClient.CommitAsync(new CancellationToken());
            
            Assert.That(success, Is.True);
            var result2 = await m_queryClient.TodoItemCategoryQueryAsync(1);
            Assert.That(result2, Is.Not.Null);
        }
        
        [Order(5)]
        [Test]
        public async Task TestTodoItemToCategoryInsertion()
        {
            await m_insertionClient.TodoItemToCategoryInsertionAsync(1, 1);
            var success = await m_insertionClient.CommitAsync(new CancellationToken());
            
            Assert.That(success, Is.True);
            var resultItem = (await m_queryClient.TodoItemQueryAsync(1)).CategorySet.ToList();
            Assert.That(resultItem, Has.Count.EqualTo(1));
            Assert.That(resultItem.First().TodoItemCategoryId, Is.EqualTo(1));

            var resultCategory = (await m_queryClient.TodoItemCategoryQueryAsync(1))
                .TodoItemSet.ToList();
            Assert.That(resultCategory, Has.Count.EqualTo(1));
            Assert.That(resultCategory.First().TodoItemId, Is.EqualTo(1));
            
            await m_insertionClient.TodoItemToCategoryInsertionAsync(1, 2);
            success = await m_insertionClient.CommitAsync(new CancellationToken());
            
            Assert.That(success, Is.True);
            resultCategory = (await m_queryClient.TodoItemCategoryQueryAsync(1)).TodoItemSet.ToList();
            Assert.That(resultCategory, Has.Count.EqualTo(2));
        }
        
        [Order(6)]
        [Test]
        public async Task TestTodoItemModification()
        {
            var todoItem = BasicTodoData.TodoItems[0];
            todoItem.Title = "Modified Title";
            todoItem.Description = "Modified Description";
            await m_modificationClient.TodoItemModificationAsync(1, todoItem);
            var success = await m_modificationClient.CommitAsync(new CancellationToken());
            
            Assert.That(success, Is.True);
            var result = await m_queryClient.TodoItemQueryAsync(1);
            Assert.That((result.Title, result.Description),
                Is.EqualTo(("Modified Title", "Modified Description")));
            
            // Test modifying non existing.
            Assert.That(async () => await m_modificationClient.TodoItemModificationAsync(9, todoItem), 
                Throws.Nothing);
        }
        
        [Order(7)]
        [Test]
        public async Task TestDailyTodoItemModification()
        {
            var dTodoItem = BasicTodoData.DailyTodoItems[0];
            dTodoItem.TodoDate = new DateTime(2020, 6, 11);
            dTodoItem.TimeUsedForTodo = new TimeSpan(2, 0, 0);
            dTodoItem.TimeReservedForTodo = new TimeSpan(3, 0, 0);
            await m_modificationClient.DailyTodoItemModificationAsync(1, dTodoItem);
            var success = await m_modificationClient.CommitAsync(new CancellationToken());
            
            Assert.That(success, Is.True);
            var result = await m_queryClient.DailyTodoItemQueryAsync(1);
            Assert.That(result.TodoDate, 
                Is.EqualTo(new DateTime(2020, 6, 11)));
            Assert.That(result.TimeUsedForTodo, 
                Is.EqualTo(new TimeSpan(2, 0, 0)));
            Assert.That(result.TimeReservedForTodo, 
                Is.EqualTo(new TimeSpan(3, 0, 0)));
            
            // Test modifying non existing.
            Assert.That(async () => await m_modificationClient.DailyTodoItemModificationAsync(9, dTodoItem), 
                Throws.Nothing);
        }
        
        [Order(8)]
        [Test]
        public async Task TestDailyTodoItemBlockModification()
        {
            var dTodoItemBlock = BasicTodoData.DailyTodoItemBlocks[2];
            dTodoItemBlock.StartTime = new DateTime(2020, 6, 9, 16, 0, 0);
            dTodoItemBlock.EndTime = new DateTime(2020, 6, 9, 17, 0, 0);
            dTodoItemBlock.IsCompleted = true;
            await m_modificationClient.DailyTodoItemBlockModificationAsync(3, dTodoItemBlock);
            var success = await m_modificationClient.CommitAsync(new CancellationToken());
            
            Assert.That(success, Is.True);
            var result = await m_queryClient.DailyTodoItemBlockQueryAsync(3);
            Assert.That(result.StartTime, 
                Is.EqualTo(new DateTime(2020, 6, 9, 16, 0, 0)));
            Assert.That(result.EndTime, 
                Is.EqualTo(new DateTime(2020, 6, 9, 17, 0, 0)));
            Assert.That(result.IsCompleted, Is.EqualTo(true));
            
            // Test modifying non existing.
            Assert.That(
                async () => await m_modificationClient.DailyTodoItemBlockModificationAsync(9, dTodoItemBlock), 
                Throws.Nothing);
        }
        
        [Order(9)]
        [Test]
        public async Task TestTodoItemCategoryModification()
        {
            var todoItemCategory = BasicTodoData.TodoItemCategories[0];
            todoItemCategory.Title = "Modified Title";
            todoItemCategory.Description = "Modified Description";
            await m_modificationClient.TodoItemCategoryModificationAsync(1, todoItemCategory);
            var success = await m_modificationClient.CommitAsync(new CancellationToken());
            
            Assert.That(success, Is.True);
            var result = await m_queryClient.TodoItemCategoryQueryAsync(1);
            Assert.That((result.Title, result.Description),
                Is.EqualTo(("Modified Title", "Modified Description")));
            
            // Test modifying non existing.
            Assert.That(
                async () => await m_modificationClient.TodoItemCategoryModificationAsync(9, todoItemCategory), 
                Throws.Nothing);
        }

        [Order(10)]
        [Test]
        public async Task TestTodoItemDeletion()
        {
            // TODO Error handling when deleting item that has references.
            // await m_deletionClient.TodoItemDeletionAsync(1);
            
            var result1 = await m_queryClient.TodoItemQueryAsync(2);
            Assert.That(result1, Is.Not.Null);
            
            await m_deletionClient.TodoItemDeletionAsync(2);
            var success = await m_deletionClient.CommitAsync(new CancellationToken());
            
            Assert.That(success, Is.True);
            var result2 = await m_queryClient.TodoItemQueryAsync(2);
            Assert.That(result2, Is.Null);
            
            var item = await m_queryClient.TodoItemCategoryQueryAsync(1);
            Assert.That(item.TodoItemSet.All(x => x.TodoItemId != 2));
            
            // Test deleting non existing.
            Assert.That(async () => await m_deletionClient.TodoItemDeletionAsync(9), Throws.Nothing);
        }
        
        [Order(11)]
        [Test]
        public async Task TestDailyTodoItemDeletion()
        {
            // TODO Error handling when deleting item that has references.
            // await m_deletionClient.DailyTodoItemDeletionAsync(1);
            
            var result1 = await m_queryClient.DailyTodoItemQueryAsync(2);
            Assert.That(result1, Is.Not.Null);
            
            await m_deletionClient.DailyTodoItemDeletionAsync(2);
            var success = await m_deletionClient.CommitAsync(new CancellationToken());
            
            Assert.That(success, Is.True);
            var result2 = await m_queryClient.DailyTodoItemQueryAsync(2);
            Assert.That(result2, Is.Null);
            
            // Test deleting non existing.
            Assert.That(async () => await m_deletionClient.DailyTodoItemDeletionAsync(9), Throws.Nothing);
        }
        
        [Order(12)]
        [Test]
        public async Task TestDailyTodoItemBlockDeletion()
        {
            var result1 = await m_queryClient.DailyTodoItemBlockQueryAsync(1);
            Assert.That(result1, Is.Not.Null);
            
            await m_deletionClient.DailyTodoItemBlockDeletionAsync(1);
            var success = await m_deletionClient.CommitAsync(new CancellationToken());
            
            Assert.That(success, Is.True);
            var result2 = await m_queryClient.DailyTodoItemBlockQueryAsync(1);
            Assert.That(result2, Is.Null);
            
            // Test deleting non existing.
            Assert.That(async () => await m_deletionClient.DailyTodoItemBlockDeletionAsync(9), Throws.Nothing);
        }
        
        [Order(13)]
        [Test]
        public async Task TestTodoItemCategoryDeletion()
        {
            var result1 = await m_queryClient.TodoItemCategoryQueryAsync(1);
            Assert.That(result1, Is.Not.Null);
            
            await m_deletionClient.TodoItemCategoryDeletionAsync(1);
            var success = await m_deletionClient.CommitAsync(new CancellationToken());
            
            Assert.That(success, Is.True);
            var result2 = await m_queryClient.TodoItemCategoryQueryAsync(1);
            Assert.That(result2, Is.Null);
            
            var item = await m_queryClient.TodoItemQueryAsync(1);
            Assert.That(item.CategorySet.All(x => x.TodoItemCategoryId != 1));
            
            // Test deleting non existing.
            Assert.That(async () => await m_deletionClient.TodoItemCategoryDeletionAsync(9), Throws.Nothing);
        }
    }
}