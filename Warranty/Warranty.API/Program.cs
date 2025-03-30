using Amazon.S3;
using Amazon;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Warranty.API;
using Warranty.API.Extensions;
using Warranty.Core;
using Warranty.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});
builder.Services.AddDbContext<DataContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 3))
    ));
builder.Services.AddSwagger();
builder.Services.ConfigureServices();

var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
Console.WriteLine($"Connection String: {connectionString}");


builder.AddJwtAuthentication();
builder.AddJwtAuthorization();
builder.Services.AddAllowAnyCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(CorsExtensions.MyAllowSpecificOrigins);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
