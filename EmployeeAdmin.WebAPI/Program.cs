using Microsoft.EntityFrameworkCore;
using EmployeeAdmin.Persistence.Repositories;
using EmployeeAdmin.Application.Query;
using EmployeeAdmin.Persistence.Data;
using EmployeeAdmin.Persistence.Context;
using EmployeeAdmin.Application;
using EmployeeAdmin.Persistence;
using EmployeeAdmin.WebAPI;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<EmployeePositionRepository>();
builder.Services.AddScoped<EmployeePositionDataAccess>();

builder.Services.AddControllers();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services
    .AddApplication()
    .AddPersistence();


builder.Services.AddSingleton<DapperContext>();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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