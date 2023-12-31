using Microsoft.EntityFrameworkCore;
using SalesApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<SalesContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("SalesDb")));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var salesContext = scope.ServiceProvider.GetRequiredService<SalesContext>();
        salesContext.Database.EnsureCreated();
        salesContext.Seed();
    }
}
// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
