using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class GroupEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public uint RowVersion { get; set; }
    }
}
