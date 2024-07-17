using BarberShopApi.Domain.Repositories;
using BarberShopApi.Filters;
using BarberShopApi.Infrastructure.Persistence;
using BarberShopApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("BarberDb"));
builder.Services.AddScoped<IBarberShopRepository, BarberShopRepository>();
builder.Services.AddScoped<IBarberRepository, BarberRepository>();


builder.Services.AddMvc(options => options.Filters.Add(typeof(BarberExceptionFilter)));

//O  reference handler preserve nao deixa dar conflito circular
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
