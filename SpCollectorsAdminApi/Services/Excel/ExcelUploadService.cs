using Microsoft.EntityFrameworkCore;
using SpCollectorsAdminApi.Data;
using SpCollectorsAdminApi.Services.Interfaces;
namespace SpCollectorsAdminApi.Services.Excel
{
    public class ExcelUploadService : IExcelUploadService
    {
        private readonly ExcelParsel _excelParsel;
        private readonly AppDbContext _dbContext;

        public ExcelUploadService(AppDbContext dbContext, ExcelParsel excelParsel)
        {
            _excelParsel = excelParsel;
            _dbContext = dbContext;
        }

        public async Task<List<CollectorEntry>> HandleUploadAsync(IFormFile file)
        {
            ValidateFile(file);

            string period = ExtractPeriodFromFilename(file.FileName);

            using var stream = file.OpenReadStream();
            var collectorEntries = _excelParsel.ParseExcel(stream, file.FileName);

            AttachPeriodAndRelationships(collectorEntries, period);

            await ReplaceExistingPeriodDataAsync(period, collectorEntries);

            return collectorEntries;
        }

        // Validate file presence and format
        private static void ValidateFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("No file uploaded.");

            if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Invalid file format. Only .xlsx is supported.");
        }

        // Attach Period and setup relationship
        private static void AttachPeriodAndRelationships(List<CollectorEntry> entries, string period)
        {
            foreach (var collector in entries)
            {
                collector.Period = period;
                foreach (var entry in collector.Entries)
                {
                    entry.CollectorEntry = collector;
                }
            }
        }

        // Remove old entries for the same period and insert new ones
        public  async Task ReplaceExistingPeriodDataAsync(string period, List<CollectorEntry> newEntries)
        {
            var oldEntries = _dbContext.CollectorEntry.Where(c => c.Period == period);
            _dbContext.CollectorEntry.RemoveRange(oldEntries);
            await _dbContext.SaveChangesAsync();

            await _dbContext.CollectorEntry.AddRangeAsync(newEntries);
            await _dbContext.SaveChangesAsync();
        }

        public static string ExtractPeriodFromFilename(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename))
                throw new ArgumentException("Filename cannot be null or empty.");

            // Extract the month and year using a regex pattern
            var match = System.Text.RegularExpressions.Regex.Match(filename, @"([A-Z]+)(\d{4})", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            if (match.Success)
            {
                var month = match.Groups[1].Value.ToUpper(); // Extract the month part
                var year = match.Groups[2].Value;           // Extract the year part

                return $"{month}{year}";
            }

            throw new InvalidOperationException("Filename does not contain a valid period format (e.g., APRIL2025).");
        }

        public class UploadResult
        {
            public List<CollectorEntry> CollectorEntries { get; set; } = new();
            public List<AgingSummaryEntity> AgingSummaries { get; set; } = new();
            public List<FinancialSummaryEntity> FinancialSummaries { get; set; } = new();
        }


    }
}

