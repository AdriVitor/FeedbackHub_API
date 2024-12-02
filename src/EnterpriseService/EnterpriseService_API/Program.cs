using EnterpriseService_API.BackgroundServices;
using EnterpriseService_Application.Services;
using EnterpriseService_Application.Services.Interfaces;
using EnterpriseService_Infraestructure.Extensions;
using EnterpriseService_Infraestructure.Repositories;
using EnterpriseService_Infraestructure.Repositories.Interfaces;
using RabbitMQ_Lib;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddInfraestructureDBSqlServer(builder.Configuration);

//Services - Dependency Injection
builder.Services.AddScoped<IEnterpriseService, EnterpriseService>();
builder.Services.AddSingleton<RabbitMQService>();

//Repositories - Dependency Injection
builder.Services.AddScoped<IEnterpriseRepository, EnterpriseRepository>();

//BackgroundServices - Dependency Injection
builder.Services.AddHostedService<EnterpriseBackgroundService>();

// Add services to the container.
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

app.MapControllers();

using(var scopeRabbitMq = app.Services.CreateScope())
{
    var rabbitMQService = scopeRabbitMq.ServiceProvider.GetRequiredService<RabbitMQService>();

    rabbitMQService.ConfigureQueue("testefila", true, false, false, "teste1");
    rabbitMQService.ConfigureQueue("testefila2", true, false, false, "teste2");
    rabbitMQService.QueueBind("testefila", "teste1", "routingkey");
    rabbitMQService.QueueBind("testefila2", "teste2", "routingkey2");
}

app.Run();