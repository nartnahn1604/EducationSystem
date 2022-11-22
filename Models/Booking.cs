using System;
using System.Collections.Generic;

namespace IT008_UIT.Models
{
    public partial class Booking
    {
        public int BookingId { get; set; }
        public int? PtcontractId { get; set; }
        public int? CustomerId { get; set; }
        public int? Ptid { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Pt? Pt { get; set; }
        public virtual Ptcontract? Ptcontract { get; set; }
        public virtual Ptcourse? PtcontractNavigation { get; set; }
    }
}
