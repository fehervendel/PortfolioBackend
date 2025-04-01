using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Portfolio.Context;
using Portfolio.Controllers;
using Portfolio.Models;
using Portfolio.Repositories;

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

void InitializeDb()
{
    using var db = new PortfolioContext();
    InitializeCities();

    void InitializeCities()
    {
        db.Add(new Card { Title = "React", Color = "#c60af5", Order = 1, Description = "Popular and powerful frontend library."});
        /*var card = db.Cards.Find(2);
        db.Cards.Remove(card);*/
        db.SaveChanges();
    }

}

/*InitializeDb();*/

app.Run();