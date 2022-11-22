using System;
using System.Collections.Generic;

namespace IT008_UIT.Models
{
    public partial class Pt
    {
        public Pt()
        {
            Bookings = new HashSet<Booking>();
            Ptcontracts = new HashSet<Ptcontract>();
        }

        public int Ptid { get; set; }
        public string? Name { get; set; }
        public string? IdentityNumber { get; set; }
        public string? Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Avatar { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Ptcontract> Ptcontracts { get; set; }
    }
}
