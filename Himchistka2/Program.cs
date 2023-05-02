using Himchistka.Api.Mapper;
using Himchistka.Data;
using Himchistka.Data.Entities;
using Himchistka.Services.Interfaces;
using Himchistka.Services.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
                opt.UseNpgsql(connectionString, b => b.MigrationsAssembly("Himchistka.Api")));

// Add services to the container.
builder.Services.AddScoped<IOrderServices, OrderServices>();
builder.Services.AddScoped<DbContext, ApplicationDbContext>();


builder.Services.AddAutoMapper(typeof(AppMappingProfile));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
