using NUnit.Framework;
using Planner.Application.TodoManagement.DataStore.Core;
using Planner.Application.TodoManagement.DataStore.DataStoreDeletion;
using Planner.Application.TodoManagement.DataStore.DataStoreInsertion;
using Planner.Application.TodoManagement.DataStore.DataStoreModification;
using Planner.Application.TodoManagement.DataStore.DataStoreQuery;

namespace Planner.NUnitTests.ApplicationTests.TodoManagement.DataStore.InMemory
{
    [TestFixture]
    public class InMemoryCommandTests : CommandTests
    {
        [OneTimeSetUp]
        public void Init()
        {
            var mockRepo = new MockDataRepo(false);
            m_deletionClient = new InMemoryDataDeletion(mockRepo);
            m_insertionClient = new InMemoryDataInsertion(mockRepo);
            m_modificationClient = new InMemoryDataModification(mockRepo);
            m_queryClient = new InMemoryDataQuery(mockRepo);
        }
    }
}