using Microsoft.EntityFrameworkCore;
using Portfolio.Context;
using Portfolio.Models;

namespace Portfolio.Repositories;

public class ContentRepository : IContentRepository
{

    private readonly PortfolioContext _dbContext;

    public ContentRepository(PortfolioContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<Content>> GetContent()
    {
        return await _dbContext.Content.ToListAsync();
    }

    public async Task<Content> GetById(int id)
    {
        return await _dbContext.Content.FirstAsync(c => c.Id == id);
    }

    public async Task Add(Content content)
    {
        _dbContext.Add(content);
        await _dbContext.SaveChangesAsync();
    }

    public async Task EditContent(int id, string textContent, string sectionId, int order)
    {
        Content contentToEdit = await _dbContext.Content.FirstAsync(c => c.Id == id);

        contentToEdit.TextContent = textContent;
        contentToEdit.SectionId = sectionId;
        contentToEdit.Order = order;

        _dbContext.Update(contentToEdit);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteById(int id)
    {
        Content contentToDelete = await _dbContext.Content.FirstAsync(c => c.Id == id);

        _dbContext.Remove(contentToDelete);
        await _dbContext.SaveChangesAsync();
    }
}