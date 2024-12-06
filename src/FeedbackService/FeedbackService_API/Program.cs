using FeedbackService_Application.Services;
using FeedbackService_Application.Services.Interfaces;
using FeedbackService_Infraestructure.Extensions;
using FeedbackService_Infraestructure.Repositories;
using FeedbackService_Infraestructure.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Dependency Injection
//Repositories
builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
//Services
builder.Services.AddScoped<IFeedbackService, FeedbackService>();

builder.Services.AddInfraestructureDBSqlServer(builder.Configuration);


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
