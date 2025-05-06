using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpCollectorsAdminApi
{
    public class PlanDetail
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string? ContractNo { get; set; }
        public string? Planholder { get; set; }
        public string? Plan { get; set; }
        public string? Description { get; set; }
        public DateTime? EffectiveDate { get; set; } 
        public DateTime? DueDate { get; set; }
        public double? QuotaComm { get; set; }
        public double? QuotaNComm { get; set; }
        public string? CBI { get; set; }
        public string? InstNo { get; set; }
        public int? Aging { get; set; } 
        public double? Balance { get; set; }
        public double? Tax { get; set; } 
        public string? Ins { get; set; }
        public string? ORNo { get; set; }
        public DateTime? ORDate { get; set; } 
        public double? CollDue { get; set; } 
        public double? CollAdvance { get; set; }
        public int CollectorEntryId { get; set; }
        public CollectorEntry CollectorEntry { get; set; } = null!;
    }

}
