using System;
using System.Collections.Generic;

namespace IT008_UIT.Models
{
    public partial class Contract
    {
        public int ContractId { get; set; }
        public int? CustomerId { get; set; }
        public int? CourseId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; }

        public virtual Course? Course { get; set; }
        public virtual Customer? Customer { get; set; }
    }
}
