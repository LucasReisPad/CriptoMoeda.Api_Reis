using Adapter.MercadoBitcoinAdapter;
using DataAcess.Data;
using Api;
using Application;
using baseMap;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection.MercadoBitcoinAdapter;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//DbContext
builder.Services.AddDbContext<AppDbContext>(options=>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
});

//Service
builder.Services.AddScoped<ICriptoMoedaService, CriptoMoedaService>();

//Adapter
builder.Services.AddMercadoBitcoinAdapter(configuration.
    SafeGet<MercadoBitcoinAdapterConfiguration>());

builder.Services.AddAutoMapperCustomizado();

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
