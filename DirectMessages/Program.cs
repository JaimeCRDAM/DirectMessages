using DirectMessages;
using DirectMessages.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using DirectMessages.Models;
using DirectMessages.Repository;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DirectMessages.NetWorking;
using GenericTools;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    var config = new ConfigurationManager();// Configure JWT Bearer Auth to expect our security key
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = config["JwtSettings:Issuer"],
        ValidAudience = config["JwtSettings:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config["JwtSettings:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
    };
});
builder.Services.AddSingleton<FireBase>();
builder.Services.AddSingleton<HttpClient>();


builder.Services.AddSingleton<IHttpApiRest, HttpApiRest>();

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

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
