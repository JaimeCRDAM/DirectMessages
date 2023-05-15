using DirectMessages;
using DirectMessages.Controllers;
using DirectMessages.Models;
using DirectMessages.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddSingleton<CassandraBuilder>();
builder.Services.AddSingleton<IBaseRepository<DirectMessageChannel>, CreateDmRepository<DirectMessageChannel>>();
builder.Services.AddSingleton<IBaseRepository<DirectMessage>, DirectMessageRepository<DirectMessage>>();

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
