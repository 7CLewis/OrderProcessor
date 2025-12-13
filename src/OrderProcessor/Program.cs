using Microsoft.EntityFrameworkCore;
using OrderProcessor.Producer.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<OrderProcessorProducerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OrderProcessorProducer")));

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
