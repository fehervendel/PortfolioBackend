using Microsoft.EntityFrameworkCore;
using Portfolio.Context;
using Portfolio.Models;

namespace Portfolio.Repositories;

public class CardRepository : ICardRepository
{
    public async Task<IEnumerable<Card>> GetCards()
    {
        using var dbContext = new PortfolioContext();
        return await dbContext.Cards.ToListAsync();
    }

    public async Task<Card> GetById(int id)
    {
        using var dbContext = new PortfolioContext();
        return await dbContext.Cards.FirstAsync(c => c.Id == id);
    }

    public async Task Add(Card card)
    {
        using var dbContext = new PortfolioContext();
        dbContext.Add(card);
        await dbContext.SaveChangesAsync();
    }

    public async Task EditCard(int id, string title, string description, string color, int order)
    {
        using var dbContext = new PortfolioContext();

        Card cardToEdit = await dbContext.Cards.FirstAsync(c => c.Id == id);

        cardToEdit.Title = title;
        cardToEdit.Description = description;
        cardToEdit.Color = color;
        cardToEdit.Order = order;

        dbContext.Update(cardToEdit);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteById(int id)
    {
        using var dbContext = new PortfolioContext();
        Card cardToDelete = await dbContext.Cards.FirstAsync(c => c.Id == id);

        dbContext.Remove(cardToDelete);
        await dbContext.SaveChangesAsync();
    }
}