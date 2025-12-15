using Azure.Messaging.EventHubs.Producer;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderProcessor.Producer;
using OrderProcessor.Producer.Entities;
using OrderProcessor.Producer.Events;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services.AddDbContext<OrderProcessorProducerDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("OrderProcessorProducer"));
});

builder.Services.AddSingleton(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var cs = config["EventHubConnection"];
    return new EventHubProducerClient(cs, "orders");
});

builder.Services.AddScoped<IEventPublisher<Order>, FuncOrders>();

builder.Build().Run();