using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace Portfolio.Controllers;

[ApiController]
[Route("[controller]")]
public class ResumeController : ControllerBase
{
    private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resume", "Vendel-Feher-resume.pdf");

    [HttpGet("GetResume")]
    public IActionResult GetResume()
    {
        if (!System.IO.File.Exists(_filePath))
            return NotFound("File not found.");

        var fileBytes = System.IO.File.ReadAllBytes(_filePath);
        return File(fileBytes, "application/pdf", "Vendel-Feher-resume.pdf");
    }

    [Authorize]
    [HttpPost("ChangeResume")]
    public async Task<IActionResult> ChangeResume([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0 || !file.FileName.EndsWith(".pdf"))
            return BadRequest("Only PDF upload allowed.");

        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Resume");
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        using (var stream = new FileStream(_filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return Ok("File uploaded.");
    }
}