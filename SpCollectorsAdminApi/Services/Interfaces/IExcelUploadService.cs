namespace SpCollectorsAdminApi.Services.Interfaces
{
    public interface IExcelUploadService
    {
        Task<List<CollectorEntry>> HandleUploadAsync(IFormFile file);
    }
}
