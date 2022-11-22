using System;
using System.Collections.Generic;

namespace IT008_UIT.Models
{
    public partial class StaffId
    {
        public StaffId()
        {
            Accounts = new HashSet<Account>();
        }

        public int StaffId1 { get; set; }
        public string? Name { get; set; }
        public string? IdentityNumber { get; set; }
        public string? Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Avatar { get; set; }
        public int? RoleId { get; set; }
        public bool Active { get; set; }

        public virtual Role? Role { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
