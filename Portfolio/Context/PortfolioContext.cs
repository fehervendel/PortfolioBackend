using Microsoft.EntityFrameworkCore;
using Portfolio.Models;

namespace Portfolio.Context;

public class PortfolioContext : DbContext
{
    public DbSet<Card> Cards { get; set; }
    public DbSet<Content> Content { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=localhost,1434;Database=Portfolio;User Id=sa;Password=YourStrong!Passw0rd;");
    }
}