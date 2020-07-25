using Business.Models;
using Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Implementation.Services
{
    public class InMemoryGroupsService : IGroupsService
    {
        private List<Group> _groups = new List<Group>();
        private long _currentId = 0;

        public Task<IReadOnlyCollection<Group>> GetAllAsync(CancellationToken ct)
        {
            return Task.FromResult<IReadOnlyCollection<Group>>(_groups.AsReadOnly());
        }

        public async Task<Group> GetByIdAsync(long Id, CancellationToken ct)
        {
            await Task.Delay(5000, ct);

            return _groups.SingleOrDefault(g => g.Id == Id);
        }

        public Task<Group> AddAsync(Group group, CancellationToken ct)
        {
            group.Id = ++_currentId;
            _groups.Add(group);
            return Task.FromResult(group);
        }

        public Task<Group> UpdateAsync(Group group, CancellationToken ct)
        {
            var toUpdate = _groups.SingleOrDefault(g => g.Id == group.Id);
            if (toUpdate == null)
            {
                return null;
            }

            toUpdate.Name = group.Name;
            return Task.FromResult(toUpdate);
        }

        public Task RemoveAsync(long id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
