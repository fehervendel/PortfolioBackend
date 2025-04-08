using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DotNetEnv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OtpNet;
using Portfolio.Models.DTOs;
using Portfolio.Services;

namespace Portfolio.Controllers;

[ApiController]
[Route("[controller]")]

public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        Env.Load();
        string secret = Environment.GetEnvironmentVariable("GOOGLE_AUTH_KEY");

        var totp = new Totp(Base32Encoding.ToBytes(secret));

        var isValid = totp.VerifyTotp(request.Code, out _, VerificationWindow.RfcSpecifiedNetworkDelay);

        if (!isValid)
        {
            return Unauthorized("Invalid code");
        }


        var token = _authService.GenerateJwt();
        return Ok(new { token });
    }
}