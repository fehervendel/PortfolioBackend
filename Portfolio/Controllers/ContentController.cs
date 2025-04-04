using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using Portfolio.Models.DTOs;
using Portfolio.Repositories;

namespace Portfolio.Controllers;
[ApiController]
[Route("[controller]")]

public class ContentController : ControllerBase
{
    private readonly IContentRepository _contentRepository;
    private readonly ILogger<ContentController> _logger;

    public ContentController(IContentRepository contentRepository, ILogger<ContentController> logger)
    {
        _contentRepository = contentRepository;
        _logger = logger;
    }

    [HttpGet("GetContent")]
    public async Task<ActionResult<Content>> GetContent()
    {
        try
        {
            var content = await _contentRepository.GetContent();
            return Ok(content);
        }
        catch (Exception e)
        {
            _logger.LogError(e,"Error getting content");
            return NotFound("Error getting content");
        }
    }

    [HttpGet("GetContentById")]
    public async Task<ActionResult<Card>> GetContentById(int id)
    {
        try
        {
            var content = await _contentRepository.GetById(id);
            return Ok(content);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting content");
            return NotFound("Error getting content");
        }
    }

    [HttpPost("AddContent")]
    public async Task<ActionResult> AddContent([FromBody] AddContent content)
    {
        try
        {
            Content contentToAdd = new Content
            {
                TextContent = content.TextContent,
                SectionId = content.SectionId,
                Order = content.Order
            };

            await _contentRepository.Add(contentToAdd);

            return Ok(contentToAdd);
        }
        catch (Exception e)
        {
            _logger.LogError(e,"Error adding content");
            return StatusCode(500,"Error adding content");
        }
    }


    [HttpPut("EditContent")]
    public async Task<IActionResult> EditContent([FromBody] EditContent editContent)
    {
        try
        {
            Content contentToEdit = await _contentRepository.GetById(editContent.Id);

            await _contentRepository.EditContent(editContent.Id, editContent.TextContent, editContent.SectionId, editContent.Order);
            return Ok("Content has been updated successfully!");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error editing content");
            return StatusCode(500, "Error editing content");
        }
    }

    [HttpDelete("DeleteContentById")]
    public async Task<IActionResult> DeleteById(int id)
    {
        try
        {
            Content content = await _contentRepository.GetById(id);
            await _contentRepository.DeleteById(id);
            return Ok($"Content with id: {id} has been deleted!");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting content");
            return StatusCode(500, "Error deleting content");
        }
    }
}