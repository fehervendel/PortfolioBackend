using Microsoft.EntityFrameworkCore;
using Portfolio.Context;
using Portfolio.Models;

namespace Portfolio.Repositories;

public class ContentRepository : IContentRepository
{
    public async Task<IEnumerable<Content>> GetContent()
    {
        using var dbContext = new PortfolioContext();
        return await dbContext.Content.ToListAsync();
    }

    public async Task<Content> GetById(int id)
    {
        using var dbContext = new PortfolioContext();
        return await dbContext.Content.FirstAsync(c => c.Id == id);
    }

    public async Task Add(Content content)
    {
        using var dbContext = new PortfolioContext();
        dbContext.Add(content);
        await dbContext.SaveChangesAsync();
    }

    public async Task EditContent(int id, string textContent, string sectionId, int order)
    {
        using var dbContext = new PortfolioContext();

        Content contentToEdit = await dbContext.Content.FirstAsync(c => c.Id == id);

        contentToEdit.TextContent = textContent;
        contentToEdit.SectionId = sectionId;
        contentToEdit.Order = order;

        dbContext.Update(contentToEdit);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteById(int id)
    {
        using var dbContext = new PortfolioContext();
        Content contentToDelete = await dbContext.Content.FirstAsync(c => c.Id == id);

        dbContext.Remove(contentToDelete);
        await dbContext.SaveChangesAsync();
    }
}