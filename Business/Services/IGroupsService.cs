using Business.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IGroupsService
    {
        Task<IReadOnlyCollection<Group>> GetAllAsync(CancellationToken ct);

        Task<Group> GetByIdAsync(long Id, CancellationToken ct);

        Task<Group> UpdateAsync(Group group, CancellationToken ct);

        Task<Group> AddAsync(Group group, CancellationToken ct);

        Task RemoveAsync(long id, CancellationToken ct);
    }
}
