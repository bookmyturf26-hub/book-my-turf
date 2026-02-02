using BookMyTurfwebservices.Extensions;
using BookMyTurfwebservices.Middleware;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure settings
builder.Services.Configure<BookMyTurfwebservices.Models.Settings.PaymentGatewaySettings>(
    builder.Configuration.GetSection("PaymentGateway"));
builder.Services.Configure<BookMyTurfwebservices.Models.Settings.EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));

// Add DbContext
builder.Services.AddDbContext<BookMyTurfwebservices.Data.ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add application services (FIXED: no configuration parameter needed)
builder.Services.AddApplicationServices();

var app = builder.Build();

// Configure pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseAuthorization();
app.MapControllers();

// Apply migrations
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BookMyTurfwebservices.Data.ApplicationDbContext>();
    await dbContext.Database.MigrateAsync();
}

app.Run();