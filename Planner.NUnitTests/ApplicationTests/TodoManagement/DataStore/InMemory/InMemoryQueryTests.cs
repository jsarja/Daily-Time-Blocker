using NUnit.Framework;
using Planner.Application.TodoManagement.DataStore.Core;
using Planner.Application.TodoManagement.DataStore.DataStoreQuery;

namespace Planner.NUnitTests.ApplicationTests.TodoManagement.DataStore.InMemory
{
    [TestFixture]
    public class InMemoryQueryTests : QueryTests
    {
        [SetUp]
        public void QueryInit()
        {
            m_queryClient = new InMemoryDataQuery(new MockDataRepo());
        }
    }
}