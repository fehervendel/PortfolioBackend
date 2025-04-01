using Portfolio.Models;

namespace Portfolio.Repositories;

public interface IContentRepository
{
    Task<IEnumerable<Content>> GetContent();
    Task<Content> GetById(int id);
    Task Add(Content content);
    Task EditContent(int id, string textContent, string sectionId, int order);
    Task DeleteById(int id);
}