using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using Portfolio.Models.DTOs;
using Portfolio.Repositories;
using Portfolio.Services;

namespace Portfolio.Controllers;
[ApiController]
[Route("[controller]")]

public class EmailController : ControllerBase
{
    private readonly IEmailRepository _emailRepository;
    private readonly ILogger<EmailController> _logger;
    private readonly EmailService _emailService;

    public EmailController(IEmailRepository emailRepository, ILogger<EmailController> logger, EmailService emailService)
    {
        _emailRepository = emailRepository;
        _logger = logger;
        _emailService = emailService;
    }

    [HttpGet("GetEmails")]
    public async Task<ActionResult<Email>> GetEmails()
    {
        try
        {
            var emails = await _emailRepository.GetEmails();
            return Ok(emails);
        }
        catch (Exception e)
        {
            _logger.LogError(e,"Error getting emails");
            return NotFound("Error getting emails");
        }
    }

    [HttpGet("GetEmailById")]
    public async Task<ActionResult<Email>> GetEmailById(int id)
    {
        try
        {
            var email = await _emailRepository.GetById(id);
            return Ok(email);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting email");
            return NotFound("Error getting email");
        }

    }

    [HttpPost("AddEmail")]
    public async Task<ActionResult> AddCard([FromBody] AddEmail email)
    {
        try
        {
            Email emailToAdd = new Email
            {
               Name = email.Name,
               PhoneNumber = email.PhoneNumber,
               EmailAddress = email.EmailAddress,
               Message = email.Message
            };

            await _emailRepository.Add(emailToAdd);

            _emailService.SendEmails(email.EmailAddress, email.Name, email.PhoneNumber, email.Message);

            return Ok(emailToAdd);
        }
        catch (Exception e)
        {
            _logger.LogError(e,"Error adding email");
            return StatusCode(500,"Error adding email");
        }
    }
}