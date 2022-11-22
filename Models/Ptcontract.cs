using System;
using System.Collections.Generic;

namespace IT008_UIT.Models
{
    public partial class Ptcontract
    {
        public Ptcontract()
        {
            Bookings = new HashSet<Booking>();
        }

        public int PtcontractId { get; set; }
        public int? CustomerId { get; set; }
        public int? Ptid { get; set; }
        public int? PtcourseId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Pt? Pt { get; set; }
        public virtual Ptcourse? Ptcourse { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
