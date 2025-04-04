using Microsoft.EntityFrameworkCore;
using Portfolio.Context;
using Portfolio.Models;

namespace Portfolio.Repositories;

public class CardRepository : ICardRepository
{

    private readonly PortfolioContext _dbContext;

    public CardRepository(PortfolioContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<Card>> GetCards()
    {
        return await _dbContext.Cards.ToListAsync();
    }

    public async Task<Card> GetById(int id)
    {
        return await _dbContext.Cards.FirstAsync(c => c.Id == id);
    }

    public async Task Add(Card card)
    {
        _dbContext.Add(card);
        await _dbContext.SaveChangesAsync();
    }

    public async Task EditCard(int id, string title, string description, string color, int order)
    {
        Card cardToEdit = await _dbContext.Cards.FirstAsync(c => c.Id == id);

        cardToEdit.Title = title;
        cardToEdit.Description = description;
        cardToEdit.Color = color;
        cardToEdit.Order = order;

        _dbContext.Update(cardToEdit);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteById(int id)
    {
        Card cardToDelete = await _dbContext.Cards.FirstAsync(c => c.Id == id);

        _dbContext.Remove(cardToDelete);
        await _dbContext.SaveChangesAsync();
    }
}