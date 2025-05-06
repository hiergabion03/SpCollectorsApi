using Microsoft.EntityFrameworkCore;
using SpCollectorsAdminApi.Data;
using SpCollectorsAdminApi.Services.Excel;
using SpCollectorsAdminApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(opt => {
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000", policy =>
        policy.WithOrigins("http://localhost:3000", "https://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod());
});
builder.Services.AddScoped<ExcelParsel>();
builder.Services.AddScoped<IExcelUploadService, ExcelUploadService>();

var app = builder.Build();

app.UseCors("AllowLocalhost3000");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sp Collectors API V1");
        c.RoutePrefix = "swagger"; // Set Swagger UI at app root
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Auto-migrate database on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.Run();