using Portfolio.Models;

namespace Portfolio.Repositories;

public interface IEmailRepository
{
    Task<IEnumerable<Email>> GetEmails();
    Task Add(Email email);
    Task<Email> GetById(int id);
}