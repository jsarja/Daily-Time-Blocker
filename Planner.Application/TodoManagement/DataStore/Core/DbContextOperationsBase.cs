using System.Threading;
using System.Threading.Tasks;
using Planner.Application.Common.Interfaces;

namespace Planner.Application.TodoManagement.DataStore.Core
{
    public class DbContextOperationsBase
    {
        protected readonly IApplicationDbContext m_dbContext;
        
        protected DbContextOperationsBase(IApplicationDbContext dbContext)
        {
            m_dbContext = dbContext;
        }
        
        // DB Context should be injected once per http request => every data store class has the same context
        // => Calling commit from one data store object should save changes made through all data store objects
        // => Need to test this "theory" with unit test and/or googling.
        public async Task<bool> CommitAsync(CancellationToken cancellationToken)
        {
            var res = await m_dbContext.SaveChangesAsync(cancellationToken);
            return res > 0;
        }
    }
}