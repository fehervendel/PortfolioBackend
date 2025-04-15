using System.Diagnostics;
using System.Text;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Portfolio.Context;
using Portfolio.Controllers;
using Portfolio.Models;
using Portfolio.Repositories;
using Portfolio.Services;
using OtpNet;
using Microsoft.AspNetCore.Authentication.JwtBearer;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PortfolioContext>();
builder.Services.AddScoped<ICardRepository, CardRepository>();
builder.Services.AddScoped<IContentRepository, ContentRepository>();
builder.Services.AddScoped<IEmailRepository, EmailRepository>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<AuthService>();

Env.Load();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET"))),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
        };
    });

var app = builder.Build();
app.UseCors(policy => policy
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

void GoogleAuthKeyGenerate()
{
    var key = KeyGeneration.GenerateRandomKey(20);
    var secret = Base32Encoding.ToString(key);
    Console.WriteLine("Google Authenticator secret:");
    Console.WriteLine(secret);
}

/*GoogleAuthKeyGenerate();*/  /*<---- Uncomment, run once, and copy the key to .env*/

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();