using EnterpriseService_Application.Services;
using EnterpriseService_Application.Services.Interfaces;
using EnterpriseService_Infraestructure.Extensions;
using EnterpriseService_Infraestructure.Repositories;
using EnterpriseService_Infraestructure.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddInfraestructureDBSqlServer(builder.Configuration);

//Services - Dependency Injection
builder.Services.AddScoped<IEnterpriseService, EnterpriseService>();

//Repositories - Dependency Injection
builder.Services.AddScoped<IEnterpriseRepository, EnterpriseRepository>();

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

app.Run();