using Business.Models;
using System.Collections.Generic;

namespace Business.Services
{
    public interface IGroupsService
    {
        IReadOnlyCollection<Group> GetAll();

        Group GetById(long Id);

        Group Update(Group group);

        Group Add(Group group);
    }
}
