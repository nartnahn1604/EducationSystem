using System;
using System.Collections.Generic;

namespace IT008_UIT.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Bookings = new HashSet<Booking>();
            Contracts = new HashSet<Contract>();
            Ptcontracts = new HashSet<Ptcontract>();
        }

        public int CustomerId { get; set; }
        public string? Name { get; set; }
        public string? IdentityNumber { get; set; }
        public string? Gender { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Avatar { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
        public virtual ICollection<Ptcontract> Ptcontracts { get; set; }
    }
}
