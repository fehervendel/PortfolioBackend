using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Portfolio.Models;

namespace Portfolio.Context;

public class PortfolioContext : DbContext
{
    public DbSet<Card> Cards { get; set; }
    public DbSet<Content> Content { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        Env.Load();
        optionsBuilder.UseSqlServer(
            Environment.GetEnvironmentVariable("DB_CONNECTION_STRING"));
    }
}