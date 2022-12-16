using System;
using System.Collections.Generic;

namespace IT008_UIT.Models
{
    public partial class Role
    {
        public Role()
        {
            Staffs = new HashSet<Staff>();
        }

        public int RoleId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Staff> Staffs { get; set; }
    }
}
