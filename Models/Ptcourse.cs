using System;
using System.Collections.Generic;

namespace IT008_UIT.Models
{
    public partial class Ptcourse
    {
        public Ptcourse()
        {
            Bookings = new HashSet<Booking>();
            Ptcontracts = new HashSet<Ptcontract>();
        }

        public int PtcourseId { get; set; }
        public string? Name { get; set; }
        public int? NumberOfSession { get; set; }
        public int? Price { get; set; }
        public int? Duration { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Ptcontract> Ptcontracts { get; set; }
    }
}
