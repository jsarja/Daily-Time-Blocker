using System.Threading;
using System.Threading.Tasks;

namespace Planner.Application.TodoManagement.DataStore.Core
{
    public interface IDataOperations
    {
        public Task<bool> CommitAsync(CancellationToken cancellationToken);
    }
}