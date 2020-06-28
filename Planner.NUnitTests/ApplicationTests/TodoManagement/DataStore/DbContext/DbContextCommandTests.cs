using System;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NUnit.Framework;
using Planner.Application.TodoManagement.DataStore.DataStoreDeletion;
using Planner.Application.TodoManagement.DataStore.DataStoreInsertion;
using Planner.Application.TodoManagement.DataStore.DataStoreModification;
using Planner.Application.TodoManagement.DataStore.DataStoreQuery;
using Planner.Infrastructure.Data.EntityFramework;

namespace Planner.NUnitTests.ApplicationTests.TodoManagement.DataStore.DbContext
{
    public class DbContextDataCommandTests : CommandTests
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
            
            m_dbContext.Database.EnsureDeleted();
            m_dbContext.Database.EnsureCreated();

        }

        [SetUp]
        public void SetUp()
        {
            m_deletionClient = new DbContextDataDeletion(m_dbContext);
            m_insertionClient = new DbContextDataInsertion(m_dbContext);
            m_modificationClient = new DbContextDataModification(m_dbContext);
            m_queryClient = new DbContextDataQuery(m_dbContext);
        }
        
        [OneTimeTearDown]
        public void Clean()
        {
            m_connection?.Dispose();
            m_dbContext?.Dispose();
        }
    }
}