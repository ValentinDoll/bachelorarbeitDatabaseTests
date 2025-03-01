using BachelorWebApp.Data.DBService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Logging.AddConsole().SetMinimumLevel(LogLevel.Debug);
var test = builder.Configuration.GetConnectionString("DBConnection");

builder.Services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();

builder.Services.AddScoped<ProjectDbContext>(static provider =>
    {
        var connectionFactory = provider.GetRequiredService<ISqlConnectionFactory>();
        var connection = connectionFactory.GetConnection();
        return new ProjectDbContext(connection);
    });

builder.Services.AddScoped<ProjectService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
