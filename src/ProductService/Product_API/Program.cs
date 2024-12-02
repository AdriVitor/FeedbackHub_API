using Product_Application.Services;
using Product_Application.Services.Interfaces;
using Product_Infraestructure.Extensions;
using Product_Infraestructure.Repositories;
using Product_Infraestructure.Repositories.Interfaces;
using RabbitMQ_Lib;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfraestructureDBSqlServer(builder.Configuration);

//Services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddSingleton<RabbitMQService>();
//Repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

using (var scopeRabbitMq = app.Services.CreateScope())
{
    var rabbitMQService = scopeRabbitMq.ServiceProvider.GetRequiredService<RabbitMQService>();

    rabbitMQService.ConfigureQueue("testefila", true, false, false, "teste1");
    rabbitMQService.ConfigureQueue("testefila2", true, false, false, "teste2");
    rabbitMQService.QueueBind("testefila", "teste1", "routingkey");
    rabbitMQService.QueueBind("testefila2", "teste2", "routingkey2");
}

app.Run();
