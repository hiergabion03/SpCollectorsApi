using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpCollectorsAdminApi
{
    public class CollectorEntry
    {

        public int Id { get; set; }
        public string? CollectorName { get; set; }
        public string? CollectorCode { get; set; }
        public string? Period { get; set; }
        public List<PlanDetail> Entries { get; set; } = new();
    }

}
