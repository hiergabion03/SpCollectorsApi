using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpCollectorsAdminApi
{
    public class PaymentDetail
    {
        public int Id { get; set; }
        public string? ORNo { get; set; }
        public DateTime? ORDate { get; set; }
        public double? CollDue { get; set; }
        public double? CollAdvance { get; set; }

        // Foreign key reference
        public int PlanDetailId { get; set; }
        public PlanDetail PlanDetail { get; set; } = null!;
    }

}
