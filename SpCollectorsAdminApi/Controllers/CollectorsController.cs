using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpCollectorsAdminApi.Data;
using SpCollectorsAdminApi.Services.Excel;
using SpCollectorsAdminApi.Services.Interfaces;

namespace SpCollectorsAdminApi.Controllers
{
    public class CollectorsController : BaseApiController
    {

        private readonly IExcelUploadService _uploadService;
        private readonly AppDbContext _dbContext;

        public CollectorsController(AppDbContext dbContext, IExcelUploadService uploadService)
        {
            _dbContext = dbContext;
            _uploadService = uploadService;
        }

        [HttpPost("UploadExcel")]
        public async Task<IActionResult> UploadExcel(IFormFile file)
        {
            try
            {
                var collectorEntries = await _uploadService.HandleUploadAsync(file);


                // Save each collector and its plan details
                foreach (var collector in collectorEntries)
                {
                    foreach (var entry in collector.Entries)
                    {
                        // You may need to set foreign keys manually here if using relational structure
                        _dbContext.PlanDetail.Add(entry);
                    }

                    _dbContext.CollectorEntry.Add(collector);
                }

                await _dbContext.SaveChangesAsync();


                return Ok(collectorEntries);



            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            if (file == null || file.Length == 0)
                return BadRequest("File is required.");

            try
            {
                var result = await _uploadService.HandleUploadAsync(file);
                return Ok(new
                {
                    Message = "Upload successful",
                    UploadedCount = result.Sum(c => c.Entries.Count),
                    CollectorCount = result.Count
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

    }

}

