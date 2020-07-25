using Business.Models;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Implementation.Mappings
{
    internal static class GroupMappings
    {
        public static Group ToService(this GroupEntity entity)
        {
            return entity != null ? new Group { Id = entity.Id, Name = entity.Name, RowVersion = entity.RowVersion.ToString() } : null;
        }

        public static GroupEntity ToEntity(this Group model)
        {
            if(!uint.TryParse(model.RowVersion, out var rowVersion))
            {
                rowVersion = 0;
            }

            return model != null ? new GroupEntity { Id = model.Id, Name = model.Name, RowVersion = rowVersion } : null;
        }

        public static IReadOnlyCollection<Group> ToService(this IReadOnlyCollection<GroupEntity> entities) => entities.MapCollection(ToService);
    }
}
