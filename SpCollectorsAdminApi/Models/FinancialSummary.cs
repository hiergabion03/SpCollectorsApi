namespace SpCollectorsAdminApi.Models
{
    public class FinancialSummary
    {
        public int Id { get; set; }
        public double? CommHandlingFee { get; set; }
        public double? QuotaComm { get; set; }
        public double? NCommHandlingFee { get; set; }
        public double? QuotaNComm { get; set; }
        public double? TotalTAP { get; set; }
        public double? TotalBalance { get; set; }
    }
}
