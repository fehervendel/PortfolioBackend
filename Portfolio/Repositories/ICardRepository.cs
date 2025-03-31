using Portfolio.Models;

namespace Portfolio.Repositories;

public interface ICardRepository
{
    Task<IEnumerable<Card>> GetCards();
    Task<Card> GetById(int id);
    Task Add(Card card);
    Task EditCard(int id, string title, string description, string color, int order);
    Task DeleteById(int id);
}