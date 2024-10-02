using Product_Application.Services;
using Product_Application.Services.Interfaces;
using Product_Infraestructure.Extensions;
using Product_Infraestructure.Repositories;
using Product_Infraestructure.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfraestructureDBSqlServer(builder.Configuration);

//Services
builder.Services.AddScoped<IProductService, ProductService>();

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

app.Run();
