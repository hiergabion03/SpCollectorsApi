namespace SpCollectorsAdminApi.Models
{
    public class AgingSummary
    {
        public int Id { get; set; }
        public string? Period { get; set; } = string.Empty; // e.g., "2025-05"
        public string? Bucket { get; set; } = string.Empty; // e.g., "30", "60", "90", "ADV"
        public int? Accounts { get; set; }
        public double? QuotaComm { get; set; }
        public double? QuotaNComm { get; set; }
        public int? CollectedCount { get; set; }
        public double? CollectionAmount { get; set; }
        public double? AdvanceCollection { get; set; }
    }
}
