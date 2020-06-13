using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NUnit.Framework;
using Planner.Application.Common.Interfaces;
using Planner.Application.Common.Models;
using Planner.Application.TodoManagement.DataStore.Core;
using Planner.Application.TodoManagement.DataStore.DataStoreDeletion;
using Planner.Application.TodoManagement.DataStore.DataStoreInsertion;
using Planner.Application.TodoManagement.DataStore.DataStoreModification;
using Planner.Application.TodoManagement.DataStore.DataStoreQuery;
using Planner.Application.TodoManagement.TodoActions.ReadActions;
using Planner.Infrastructure.Data.EntityFramework;
using Planner.NUnitTests.TestData;

namespace Planner.NUnitTests.ApplicationTests.TodoManagement.DataStore.DbContext
{
    [TestFixture]
    public class DbContextQueryTests : QueryTests
    {
        private DbConnection m_connection;
        private PlannerDbContext m_dbContext;
        
        [OneTimeSetUp]
        public void Init()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            
            var contextOptions = new DbContextOptionsBuilder<PlannerDbContext>()
                .UseSqlite(connection)
                .Options;
            
            m_connection = RelationalOptionsExtension.Extract(contextOptions).Connection;
            m_dbContext = new PlannerDbContext(contextOptions);
            
            Seed();
        }
        
        [SetUp]
        public void QueryInit()
        {
            m_queryClient = new DbContextDataQuery(m_dbContext);
        }
        
        [OneTimeTearDown]
        public void Clean()
        {
            m_connection?.Dispose();
            m_dbContext?.Dispose();
        }
        
        private void Seed()
        {
            m_dbContext.Database.EnsureDeleted();
            m_dbContext.Database.EnsureCreated();
            
            var mockData = new MockDataRepo();
            
            m_dbContext.TodoItems.AddRange(mockData.TodoItems);
            m_dbContext.DailyTodoItems.AddRange(mockData.DailyTodoItems);
            m_dbContext.DailyTodoItemBlocks.AddRange(mockData.DailyTodoItemBlocks);
            m_dbContext.TodoItemCategories.AddRange(mockData.TodoItemCategories);

            foreach (var c in mockData.TodoItemCategories)
            {
                foreach (var i in c.TodoItemSet)
                {
                    m_dbContext.TodoItemCategoryJoin.Add(
                        new TodoItemCategoryJoinTable
                        {
                            TodoItem = i,
                            Category = c
                        });
                }
            }

            m_dbContext.SaveChanges();
        }
    }
}