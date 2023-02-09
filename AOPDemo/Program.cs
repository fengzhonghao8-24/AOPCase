using AOPDemo;
using AOPDemo.AOP;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using IServices;
using Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>((context, buider) =>
{
    buider.RegisterModule(new AutofacModuleRegister());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("TestAdsFromAOP", (IAdvertisementServices _advertisementServices) =>
{
    var data = _advertisementServices.TestAOP();

    return data;
});

app.Run();