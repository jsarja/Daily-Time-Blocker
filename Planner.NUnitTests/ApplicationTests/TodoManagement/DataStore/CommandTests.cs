using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Planner.Application.TodoManagement.DataStore.DataStoreDeletion;
using Planner.Application.TodoManagement.DataStore.DataStoreInsertion;
using Planner.Application.TodoManagement.DataStore.DataStoreModification;
using Planner.Application.TodoManagement.DataStore.DataStoreQuery;
using Planner.Application.TodoManagement.TodoActions.ReadActions;
using Planner.Domain.Entities;
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
        public async Task TestSuccessfulTodoItemInsertion()
        {
            // Insert new item and check that it exist in store after insertion.
            var todoItem1 = BasicTodoData.TodoItems[0];
            todoItem1.CategorySet.Clear();
            await m_insertionClient.TodoItemInsertionAsync(todoItem1);
            var success = await m_insertionClient.CommitAsync(new CancellationToken());
            
            Assert.That(success, Is.True);
            var result1 = await m_queryClient.TodoItemQueryAsync(1);
            Assert.That(result1, Is.Not.Null);
            
            // Insert another item and check that it exist in store after insertion.
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
        public virtual async Task TestNonSuccessfulTodoItemInsertion()
        {
            // Insert null value and check that there is only 2 values in store after this 3rd insertion.
            Assert.That( async () => await m_insertionClient.TodoItemInsertionAsync(null),
                Throws.ArgumentNullException);
            var success = await m_insertionClient.CommitAsync(new CancellationToken());
            
            Assert.That(success, Is.False);
            var results = await m_queryClient.TodoItemsQueryAsync(new TodoItemsSearchArgs());
            Assert.That(results.Count(), Is.EqualTo(2));
            
            // Insert value without commiting and check that there is only 2 values in store after this insertion.
            var todoItem = BasicTodoData.TodoItems[2];
            todoItem.CategorySet.Clear();
            await m_insertionClient.TodoItemInsertionAsync(todoItem);
            
            results = await m_queryClient.TodoItemsQueryAsync(new TodoItemsSearchArgs());
            Assert.That(results.Count(), Is.EqualTo(2));
        }

        [Order(3)]
        [Test]
        public async Task TestSuccessfulDailyTodoItemInsertion()
        {
            // Insert new item and check that it exist in store after insertion.
            var dTodoItem1 = BasicTodoData.DailyTodoItems[0];
            await m_insertionClient.DailyTodoItemInsertionAsync(dTodoItem1);
            var success = await m_insertionClient.CommitAsync(new CancellationToken());
            
            Assert.That(success, Is.True);
            var result1 = await m_queryClient.DailyTodoItemQueryAsync(1);
            Assert.That(result1, Is.Not.Null);
            
            // Insert another item and check that it exist in store after insertion. 
            var dTodoItem2 = BasicTodoData.DailyTodoItems[1];
            await m_insertionClient.DailyTodoItemInsertionAsync(dTodoItem2);
            success = await m_insertionClient.CommitAsync(new CancellationToken());
            
            Assert.That(success, Is.True);
            var result2 = await m_queryClient.DailyTodoItemQueryAsync(2);
            Assert.That(result2, Is.Not.Null);
            
        }
        
        [Order(4)]
        [Test]
        public virtual async Task TestNonSuccessfulDailyTodoItemInsertion()
        {
            // Insert null value and check that there is only 2 values in store after this 3rd insertion.
            Assert.That( async () => await m_insertionClient.DailyTodoItemInsertionAsync(null),
                Throws.ArgumentNullException);
            var results = await m_queryClient.DailyTodoItemsQueryAsync(
                new GetDailyTodoItemsSearchArgs());
            Assert.That(results, Has.Count.EqualTo(2));
            
            // Insert value without commiting and check that there is only 2 values in store after this insertion.
            var dTodoItem = BasicTodoData.DailyTodoItems[2];
            await m_insertionClient.DailyTodoItemInsertionAsync(dTodoItem);
            results = await m_queryClient.DailyTodoItemsQueryAsync(new GetDailyTodoItemsSearchArgs());
            Assert.That(results, Has.Count.EqualTo(2));
        }
        
        [Order(5)]
        [Test]
        public async Task TestSuccessfulDailyTodoItemBlockInsertion()
        {
            // Insert new block and check that it exist in store after insertion.
            var dTodoItemBlock1 = BasicTodoData.DailyTodoItemBlocks[0];
            await m_insertionClient.DailyTodoItemBlockInsertionAsync(dTodoItemBlock1);
            var success = await m_insertionClient.CommitAsync(new CancellationToken());
            
            Assert.That(success, Is.True);
            var result1 = await m_queryClient.DailyTodoItemBlockQueryAsync(1);
            Assert.That(result1, Is.Not.Null);
            
            // Insert another block and check that it exist in store after insertion. 
            var dTodoItemBlock2 = BasicTodoData.DailyTodoItemBlocks[2];
            await m_insertionClient.DailyTodoItemBlockInsertionAsync(dTodoItemBlock2);
            success = await m_insertionClient.CommitAsync(new CancellationToken());
            
            Assert.That(success, Is.True);
            var result2 = await m_queryClient.DailyTodoItemBlockQueryAsync(3);
            Assert.That(result2, Is.Not.Null);
        }

        [Order(6)]
        [Test]
        public virtual async Task TestNonSuccessfulDailyTodoItemBlockInsertion()
        {
            // Insert null value and check that there is only 2 values in store after this 3rd insertion.
            Assert.That( async () => await m_insertionClient.DailyTodoItemBlockInsertionAsync(null),
                Throws.ArgumentNullException);
            var results = await m_queryClient.DailyTodoItemBlocksQueryAsync(
                new GetDailyTodoItemBlocksSearchArgs());
            Assert.That(results, Has.Count.EqualTo(2));
            
            // Insert value without commiting and check that there is only 2 values in store after this insertion.
            var block = BasicTodoData.DailyTodoItemBlocks[3];
            await m_insertionClient.DailyTodoItemBlockInsertionAsync(block);
            results = await m_queryClient.DailyTodoItemBlocksQueryAsync(new GetDailyTodoItemBlocksSearchArgs());
            Assert.That(results, Has.Count.EqualTo(2));
        }

        [Order(7)]
        [Test]
        public async Task TestSuccessfulTodoItemCategoryInsertion()
        {
            // Insert new category and check that it exist in store after insertion.
            var todoItemCategory1 = BasicTodoData.TodoItemCategories[0];
            todoItemCategory1.TodoItemSet.Clear();
            await m_insertionClient.TodoItemCategoryInsertionAsync(todoItemCategory1);
            var success = await m_insertionClient.CommitAsync(new CancellationToken());
            
            Assert.That(success, Is.True);
            var result1 = await m_queryClient.TodoItemCategoryQueryAsync(1);
            Assert.That(result1, Is.Not.Null);
            
            // Insert another category and check that it exist in store after insertion. 
            var todoItemCategory2 = BasicTodoData.TodoItemCategories[1];
            todoItemCategory2.TodoItemSet.Clear();
            await m_insertionClient.TodoItemCategoryInsertionAsync(todoItemCategory2);
            success = await m_insertionClient.CommitAsync(new CancellationToken());
            
            Assert.That(success, Is.True);
            var result2 = await m_queryClient.TodoItemCategoryQueryAsync(1);
            Assert.That(result2, Is.Not.Null);
        }

        [Order(8)]
        [Test]
        public virtual async Task TestNonSuccessfulTodoItemCategoryInsertion()
        {
            // Insert null value and check that there is only 2 values in store after this 3rd insertion.
            Assert.That(async () => await m_insertionClient.TodoItemCategoryInsertionAsync(null),
                Throws.ArgumentNullException);
            var results = await m_queryClient.TodoItemCategoriesQueryAsync(
                new TodoItemCategoriesSearchArgs());
            Assert.That(results, Has.Count.EqualTo(2));
            
            // Insert value without commiting and check that there is only 2 values in store after this insertion.
            var category = BasicTodoData.TodoItemCategories[1];
            category.TodoItemCategoryId = 3;
            await m_insertionClient.TodoItemCategoryInsertionAsync(category);
            results = await m_queryClient.TodoItemCategoriesQueryAsync(new TodoItemCategoriesSearchArgs());
            Assert.That(results, Has.Count.EqualTo(2));
        }

        [Order(9)]
        [Test]
        public async Task TestSuccessfulTodoItemToCategoryInsertion()
        {
            // Add item with id 1 to category with id 1.
            await m_insertionClient.TodoItemToCategoryInsertionAsync(1, 1);
            var success = await m_insertionClient.CommitAsync(new CancellationToken());
            
            // Check that todoItem has category in its category set.
            Assert.That(success, Is.True);
            var resultItem = (await m_queryClient.TodoItemQueryAsync(1)).CategorySet
                .ToHashSet();
            Assert.That(resultItem, Has.Count.EqualTo(1));
            Assert.That(resultItem.First().TodoItemCategoryId, Is.EqualTo(1));

            // Check that category has todoITem in its todoItem set.
            var resultCategory = (await m_queryClient.TodoItemCategoryQueryAsync(1))
                .TodoItemSet;
            Assert.That(resultCategory, Has.Count.EqualTo(1));
            Assert.That(resultCategory.First().TodoItemId, Is.EqualTo(1));
            
            // Add item with id 2 to category with id 1.
            await m_insertionClient.TodoItemToCategoryInsertionAsync(1, 2);
            success = await m_insertionClient.CommitAsync(new CancellationToken());
            
            Assert.That(success, Is.True);
            resultCategory = (await m_queryClient.TodoItemCategoryQueryAsync(1)).TodoItemSet;
            Assert.That(resultCategory, Has.Count.EqualTo(2));
        }
        
        [Order(10)]
        [Test]
        public virtual async Task TestNonSuccessfulTodoItemToCategoryInsertion()
        {
            // Add non-existing todoItem to category with id 1.
            await m_insertionClient.TodoItemToCategoryInsertionAsync(1, 9);
            var success = await m_insertionClient.CommitAsync(new CancellationToken());
            Assert.That(success, Is.False);
            var resultCategory = (await m_queryClient.TodoItemCategoryQueryAsync(1)).TodoItemSet;
            Assert.That(resultCategory, Has.Count.EqualTo(2));
            
            // Add todoItem with id 1 to non-existing category.
            await m_insertionClient.TodoItemToCategoryInsertionAsync(9, 1);
            await m_insertionClient.CommitAsync(new CancellationToken());
            var resultItem = (await m_queryClient.TodoItemQueryAsync(1)).CategorySet;
            Assert.That(resultItem, Has.Count.EqualTo(1));
        }

        [Order(11)]
        [Test]
        public async Task TestSuccessfulTodoItemModification()
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
        }
        
        [Order(12)]
        [Test]
        public virtual async Task TestNonSuccessfulTodoItemModification()
        {
            // Try to update non existing entity.
            var todoItem = BasicTodoData.TodoItems[3];
            todoItem.TodoItemId = 9;
            todoItem.Title = "Modified Title";
            todoItem.Description = "Modified Description";
            await m_modificationClient.TodoItemModificationAsync(9, todoItem);
            var success = await m_modificationClient.CommitAsync(new CancellationToken());
            Assert.That(success, Is.False);
            
            // Try to update with empty object.
            var originalTitle = (await m_queryClient.TodoItemQueryAsync(1)).Title;
            await m_modificationClient.TodoItemModificationAsync(1, new TodoItem());
            success = await m_modificationClient.CommitAsync(new CancellationToken());
            Assert.That(success, Is.True);
            var modifiedTitle = (await m_queryClient.TodoItemQueryAsync(1)).Title;
            Assert.That(modifiedTitle, Is.EqualTo(originalTitle));
            
            // Try to update with null objects.
            Assert.That( async () => await m_modificationClient.TodoItemModificationAsync(1, null),
                Throws.ArgumentNullException);
            var result = await m_queryClient.TodoItemQueryAsync(1);
            Assert.That(result, Is.Not.Null);

        }
        
        [Order(13)]
        [Test]
        public async Task TestSuccessfulDailyTodoItemModification()
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
        }
        
        [Order(14)]
        [Test]
        public virtual async Task TestNonSuccessfulDailyTodoItemModification()
        {
            // Try to update non existing entity.
            var dailyTodoItem = BasicTodoData.DailyTodoItems[4];
            dailyTodoItem.DailyTodoItemId = 9;
            dailyTodoItem.TodoDate = new DateTime(2020, 7, 1);
            dailyTodoItem.TimeReservedForTodo = new TimeSpan(1, 1, 1);
            await m_modificationClient.DailyTodoItemModificationAsync(9, dailyTodoItem);
            var success = await m_modificationClient.CommitAsync(new CancellationToken());
            Assert.That(success, Is.False);
            
            // Try to update with empty object.
            var originalDate = (await m_queryClient.DailyTodoItemQueryAsync(1)).TodoDate;
            await m_modificationClient.DailyTodoItemModificationAsync(1, new DailyTodoItem());
            success = await m_modificationClient.CommitAsync(new CancellationToken());
            Assert.That(success, Is.True);
            var modifiedDate = (await m_queryClient.DailyTodoItemQueryAsync(1)).TodoDate;
            Assert.That(modifiedDate, Is.EqualTo(originalDate));
            
            // Try to update with null objects.
            Assert.That( async () => await m_modificationClient.DailyTodoItemModificationAsync(1, null),
                Throws.ArgumentNullException);
            var result = await m_queryClient.DailyTodoItemQueryAsync(1);
            Assert.That(result, Is.Not.Null);
        }
        
        [Order(15)]
        [Test]
        public async Task TestSuccessfulDailyTodoItemBlockModification()
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
        }
        
        [Order(16)]
        [Test]
        public virtual async Task TestNonSuccessfulDailyTodoItemBlockModification()
        {
            // Try to update non existing entity.
            var dailyTodoItemBlock = BasicTodoData.DailyTodoItemBlocks[4];
            dailyTodoItemBlock.DailyTodoItemBlockId = 9;
            dailyTodoItemBlock.EndTime = new DateTime(2021, 1, 1);
            dailyTodoItemBlock.IsCompleted = false;
            await m_modificationClient.DailyTodoItemBlockModificationAsync(9, dailyTodoItemBlock);
            var success = await m_modificationClient.CommitAsync(new CancellationToken());
            Assert.That(success, Is.False);
            
            // Try to update with empty object.
            var originalStartTime = (await m_queryClient.DailyTodoItemBlockQueryAsync(1)).StartTime;
            await m_modificationClient.DailyTodoItemBlockModificationAsync(1, new DailyTodoItemBlock());
            success = await m_modificationClient.CommitAsync(new CancellationToken());
            Assert.That(success, Is.True);
            var modifiedStartTime = (await m_queryClient.DailyTodoItemBlockQueryAsync(1)).StartTime;
            Assert.That(originalStartTime, Is.EqualTo(modifiedStartTime));
            
            // Try to update with null objects.
            Assert.That( async () => await m_modificationClient
                    .DailyTodoItemBlockModificationAsync(1, null),
                Throws.ArgumentNullException);
            var result = await m_queryClient.DailyTodoItemBlockQueryAsync(1);
            Assert.That(result, Is.Not.Null);
        }
        
        [Order(17)]
        [Test]
        public async Task TestSuccessfulTodoItemCategoryModification()
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
        }
        
        [Order(18)]
        [Test]
        public virtual async Task TestNonSuccessfulTodoItemCategoryModification()
        {
            // Try to update non existing entity.
            var todoItemCategory = new TodoItemCategory
            {
                TodoItemCategoryId = 9,
                Title = "New modification category",
                Description = "New modification category",
                OwnerId = 1
            };
            
            await m_modificationClient.TodoItemCategoryModificationAsync(9, todoItemCategory);
            var success = await m_modificationClient.CommitAsync(new CancellationToken());
            Assert.That(success, Is.False);
            
            // Try to update with empty object.
            var originalTitle = (await m_queryClient.TodoItemCategoryQueryAsync(1)).Title;
            await m_modificationClient.TodoItemCategoryModificationAsync(1, new TodoItemCategory());
            success = await m_modificationClient.CommitAsync(new CancellationToken());
            Assert.That(success, Is.False);
            var modifiedTitle = (await m_queryClient.TodoItemCategoryQueryAsync(1)).Title;
            Assert.That(modifiedTitle, Is.EqualTo(originalTitle));
            
            // Try to update with null objects.
            Assert.That( async () => await m_modificationClient.TodoItemCategoryModificationAsync(1, null),
                Throws.ArgumentNullException);
            var result = await m_queryClient.TodoItemCategoryQueryAsync(1);
            Assert.That(result, Is.Not.Null);
        }

        [Order(19)]
        [Test]
        public async Task TestSuccessfulTodoItemDeletion()
        {
            var result1 = await m_queryClient.TodoItemQueryAsync(2);
            Assert.That(result1, Is.Not.Null);
            
            await m_deletionClient.TodoItemDeletionAsync(2);
            var success = await m_deletionClient.CommitAsync(new CancellationToken());
            
            Assert.That(success, Is.True);
            var result2 = await m_queryClient.TodoItemQueryAsync(2);
            Assert.That(result2, Is.Null);
            
            var item = await m_queryClient.TodoItemCategoryQueryAsync(1);
            Assert.That(item.TodoItemSet.All(x => x.TodoItemId != 2));
        }
        
        [Order(20)]
        [Test]
        public async Task TestNonSuccessfulTodoItemDeletion()
        {
            // TODO Error handling when deleting item that has references.
            // await m_deletionClient.TodoItemDeletionAsync(1);

            // Test deleting non existing.
            Assert.That(async () => await m_deletionClient.TodoItemDeletionAsync(9), Throws.Nothing);
        }
        
        [Order(21)]
        [Test]
        public async Task TestSuccessfulDailyTodoItemDeletion()
        {
            var result1 = await m_queryClient.DailyTodoItemQueryAsync(2);
            Assert.That(result1, Is.Not.Null);
            
            await m_deletionClient.DailyTodoItemDeletionAsync(2);
            var success = await m_deletionClient.CommitAsync(new CancellationToken());
            
            Assert.That(success, Is.True);
            var result2 = await m_queryClient.DailyTodoItemQueryAsync(2);
            Assert.That(result2, Is.Null);
        }
        
        [Order(22)]
        [Test]
        public async Task TestNonSuccessfulDailyTodoItemDeletion()
        {
            // TODO Error handling when deleting item that has references.
            // await m_deletionClient.DailyTodoItemDeletionAsync(1);
            
            // Test deleting non existing.
            Assert.That(async () => await m_deletionClient.DailyTodoItemDeletionAsync(9), Throws.Nothing);
        }
        
        [Order(23)]
        [Test]
        public async Task TestSuccessfulDailyTodoItemBlockDeletion()
        {
            var result1 = await m_queryClient.DailyTodoItemBlockQueryAsync(1);
            Assert.That(result1, Is.Not.Null);
            
            await m_deletionClient.DailyTodoItemBlockDeletionAsync(1);
            var success = await m_deletionClient.CommitAsync(new CancellationToken());
            
            Assert.That(success, Is.True);
            var result2 = await m_queryClient.DailyTodoItemBlockQueryAsync(1);
            Assert.That(result2, Is.Null);
        }
        
        [Order(24)]
        [Test]
        public async Task TestNonSuccessfulDailyTodoItemBlockDeletion()
        {
            // Test deleting non existing.
            Assert.That(async () => await m_deletionClient.DailyTodoItemBlockDeletionAsync(9), Throws.Nothing);
        }
        
        [Order(25)]
        [Test]
        public async Task TestSuccessfulTodoItemCategoryDeletion()
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
        }
        
        [Order(26)]
        [Test]
        public async Task TestNonSuccessfulTodoItemCategoryDeletion()
        {
            // Test deleting non existing.
            Assert.That(async () => await m_deletionClient.TodoItemCategoryDeletionAsync(9), Throws.Nothing);
        }
    }
}