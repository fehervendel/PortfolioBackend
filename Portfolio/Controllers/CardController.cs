using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using Portfolio.Models.DTOs;
using Portfolio.Repositories;

namespace Portfolio.Controllers;
[ApiController]
[Route("[controller]")]

public class CardController : ControllerBase
{
    private readonly ICardRepository _cardRepository;
    private readonly ILogger<CardController> _logger;

    public CardController(ICardRepository cardRepository, ILogger<CardController> logger)
    {
        _cardRepository = cardRepository;
        _logger = logger;
    }

    [HttpGet("GetCards")]
    public async Task<ActionResult<Card>> GetCards()
    {
        try
        {
            var cards = await _cardRepository.GetCards();
            return Ok(cards);
        }
        catch (Exception e)
        {
            _logger.LogError(e,"Error getting cards");
            return NotFound("Error getting cards");
        }
    }

    [HttpGet("GetCardById")]
    public async Task<ActionResult<Card>> GetCardById(int id)
    {
        try
        {
            var card = await _cardRepository.GetById(id);
            return Ok(card);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting card");
            return NotFound("Error getting card");
        }
    }

    [HttpPost("AddCard")]
    public async Task<ActionResult> AddCard([FromBody] AddCard card)
    {
        try
        {
            Card cardToAdd = new Card
            {
                Title = card.Title,
                Description = card.Description,
                Color = card.Color,
                Order = card.Order
            };

            await _cardRepository.Add(cardToAdd);

            return Ok(cardToAdd);
        }
        catch (Exception e)
        {
            _logger.LogError(e,"Error adding card");
            return StatusCode(500,"Error adding card");
        }
    }

    [HttpPut("EditCard")]
    public async Task<IActionResult> EditCard([FromBody] EditCard editCard)
    {
        try
        {
            Card cardToEdit = await _cardRepository.GetById(editCard.Id);

            await _cardRepository.EditCard(editCard.Id, editCard.Title, editCard.Description, editCard.Color,
                editCard.Order);
            return Ok("Card has been updated successfully!");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error editing card");
            return StatusCode(500, "Error editing card");
        }
    }

    [HttpDelete("DeleteCardById")]
    public async Task<IActionResult> DeleteById(int id)
    {
        try
        {
            Card card = await _cardRepository.GetById(id);
            await _cardRepository.DeleteById(id);
            return Ok($"Card with id: {id} has been deleted!");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting card");
            return StatusCode(500, "Error deleting card");
        }
    }
}