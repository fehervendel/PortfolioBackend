using Microsoft.AspNetCore.Mvc;
using Portfolio.Context;

namespace Portfolio.Controllers;

[ApiController]
[Route("[controller]")]
public class ModelController : ControllerBase
{
    private readonly PortfolioContext _context;

    public ModelController(PortfolioContext context)
    {
        _context = context;
    }

    [HttpGet("GetModels")]
    public IActionResult GetDbNames()
    {
        var dbSetNames = _context.Model.GetEntityTypes()
            .Select(e => e.Name)
            .ToList();

        return Ok(dbSetNames);
    }
}