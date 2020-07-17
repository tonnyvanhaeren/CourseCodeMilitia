using Business.Models;
using Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Implementation.Services
{
    public class InMemoryGroupsService : IGroupsService
    {
        private List<Group> _groups = new List<Group>();
        private long _currentId = 0;

        public IReadOnlyCollection<Group> GetAll()
        {
            return _groups.AsReadOnly();
        }

        public Group GetById(long Id)
        {
            return _groups.SingleOrDefault(g => g.Id == Id);

        }

        public Group Add(Group group)
        {
            group.Id = ++_currentId;
            _groups.Add(group);
            return group;
        }

        public Group Update(Group group)
        {
            var toUpdate = _groups.SingleOrDefault(g => g.Id == group.Id);
            if (toUpdate == null)
            {
                return null;
            }

            toUpdate.Name = group.Name;
            return toUpdate;
        }
    }
}
