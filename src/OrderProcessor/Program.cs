using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderProcessor.Producer.Entities;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services.AddDbContext<OrderProcessorProducerDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("OrderProcessorProducer"));
});

builder.Build().Run();