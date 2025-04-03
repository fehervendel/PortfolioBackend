using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Portfolio.Context;
using Portfolio.Controllers;
using Portfolio.Models;
using Portfolio.Repositories;
using Portfolio.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PortfolioContext>();
builder.Services.AddScoped<ICardRepository, CardRepository>();
builder.Services.AddScoped<IContentRepository, ContentRepository>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


void SendTestEmail()
{
    string userEmail = "fehervendel@gmail.com";
    string userName = "Fehér Vendel";

    EmailService emailService = new EmailService();
    emailService.SendEmails(userEmail, userName);
}

/*
SendTestEmail(); works
*/


app.Run();