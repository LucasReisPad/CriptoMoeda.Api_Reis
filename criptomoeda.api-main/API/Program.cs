using Adapter.MercadoBitcoinAdapter;
using Api;
using Application;
using baseMap;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection.MercadoBitcoinAdapter;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
