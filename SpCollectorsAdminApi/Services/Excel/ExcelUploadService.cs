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
            var collectorEntries = _excelParsel.ParseExcel(stream);

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
        private async Task ReplaceExistingPeriodDataAsync(string period, List<CollectorEntry> newEntries)
        {
            var oldEntries = _dbContext.CollectorEntry.Where(c => c.Period == period);
            _dbContext.CollectorEntry.RemoveRange(oldEntries);
            await _dbContext.SaveChangesAsync();

            await _dbContext.CollectorEntry.AddRangeAsync(newEntries);
            await _dbContext.SaveChangesAsync();
        }

        private string ExtractPeriodFromFilename(string filename)
        {
            var name = Path.GetFileNameWithoutExtension(filename).ToUpper();

            // Step 1: Find the year in the filename (we are looking for 4 digits, like "2025")
            var yearMatch = System.Text.RegularExpressions.Regex.Match(filename, @"\d{4}");

            if (yearMatch.Success)
            {
                // Step 2: Extract the part before the year
                var monthYearPart = filename.Substring(filename.LastIndexOf('_') + 1, yearMatch.Index + 4 - (filename.LastIndexOf('_') + 1));

                // Step 3: Extract the month and year
                var month = new string(monthYearPart.TakeWhile(c => char.IsLetter(c)).ToArray());
                var year = yearMatch.Value;

    
                
                return $"{month}{year}"; // e.g., "JANUARY2025"
                    
                
            }

            throw new InvalidOperationException("Filename does not contain a valid period format (e.g., JANUARY2025).");
        }


    }
}

