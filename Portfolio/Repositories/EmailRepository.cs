using Microsoft.EntityFrameworkCore;
using Portfolio.Context;
using Portfolio.Models;

namespace Portfolio.Repositories;

public class EmailRepository : IEmailRepository
{
    private readonly PortfolioContext _dbContext;

    public EmailRepository(PortfolioContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Email>> GetEmails()
    {
        return await _dbContext.Emails.ToListAsync();
    }

    public async Task Add(Email email)
    {
        _dbContext.Add(email);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Email> GetById(int id)
    {
        return await _dbContext.Emails.FirstAsync(c => c.Id == id);
    }
}