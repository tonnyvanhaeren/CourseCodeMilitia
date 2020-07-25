using Business.Implementation.Mappings;
using Business.Models;
using Business.Services;
using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Implementation.Services
{
    public class GroupService : IGroupsService
    {
        private readonly GroupManagementDbContext _context;

        public GroupService(GroupManagementDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<Group>> GetAllAsync(CancellationToken ct)
        {
            var groups = await _context.Groups.AsNoTracking().OrderBy(g => g.Id).ToListAsync(ct);
            return groups.ToService();
        }

        public async Task<Group> GetByIdAsync(long Id, CancellationToken ct)
        {
            // var group = await _context.Groups.FindAsync(new object[] { Id }, ct);
            var group = await _context.Groups.AsNoTracking().SingleOrDefaultAsync(g => g.Id == Id, ct);
            return group.ToService();
        }

        public async Task<Group> AddAsync(Group group, CancellationToken ct)
        {
            var addedGroupEntry = _context.Groups.Add(group.ToEntity());
            await _context.SaveChangesAsync(ct);
            return addedGroupEntry.Entity.ToService();
        }

        public async Task<Group> UpdateAsync(Group group, CancellationToken ct)
        {
            var existingGroup = await _context.Groups.SingleOrDefaultAsync(g => g.Id == group.Id);
            existingGroup.Name = group.Name;

            await _context.SaveChangesAsync(ct);
            return existingGroup.ToService();

            //var updatedGroupEntry = _context.Groups.Update(group.ToEntity());
            //await _context.SaveChangesAsync(ct);
            //return updatedGroupEntry.Entity.ToService();
        }

        //private async Task<Group> SimplestUpdateAsync(Group group, CancellationToken ct)
        //{
        //    var updatedGroupEntry = _context.Groups.Update(group.ToEntity());
        //    await _context.SaveChangesAsync(ct);
        //    return updatedGroupEntry.Entity.ToService();
        //}

        //private async Task<Group> UpdateWithFetchAsync(Group group, CancellationToken ct)
        //{
        //    var existingGroup = await _context.Groups.SingleOrDefaultAsync(g => g.Id == group.Id);
        //    existingGroup.Name = group.Name;

        //    await _context.SaveChangesAsync(ct);
        //    return existingGroup.ToService();
        //}

        //private async Task<Group> UpdateWithFetch2Async(Group group, CancellationToken ct)
        //{
        //    var existingGroup = await _context.Groups.SingleOrDefaultAsync(g => g.Id == group.Id);
        //    _context.Entry(existingGroup).CurrentValues.SetValues(group.ToEntity());

        //    await _context.SaveChangesAsync(ct);
        //    return existingGroup.ToService();
        //}
    }
}
