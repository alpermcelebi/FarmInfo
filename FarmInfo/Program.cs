global using Microsoft.EntityFrameworkCore;
using FarmInfo.Repositories.CowRepo;
using FarmInfo.Repositories.Data;
using FarmInfo.Services.CowService;
using FarmInfo.Services.HealthService;
using FarmInfo.Services.MilkProductionService;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); //configure the dbcontext w/ SQL server
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<ICowService, CowService>(); // register service
builder.Services.AddScoped<IHealthService, HealthService>(); // register service
builder.Services.AddScoped<IMilkProductionService, MilkProductionService>(); // register service

builder.Services.AddScoped<ICowRepository, CowRepository>(); // Register the repository

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
