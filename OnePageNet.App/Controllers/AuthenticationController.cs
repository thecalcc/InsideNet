using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnePageNet.App.Data;
using OnePageNet.App.Data.Entities;
using OnePageNet.App.Data.Models;
using OnePageNet.App.Services.Interfaces;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace OnePageNet.App.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : Controller
{
    private readonly ILogger _logger;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IEmailService _emailService;

    private readonly ITokenService _tokenService;

    // TODO Fix this - don't inject the database into the controller directly !!!!!!!!!!!!!!!!!!!
    private readonly OnePageNetDbContext _dbContext;
    private readonly IConfiguration _configuration;
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthenticationController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IEmailService emailService,
        ITokenService tokenService,
        OnePageNetDbContext dbContext,
        IConfiguration configuration,
        ILoggerFactory loggerFactory)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _emailService = emailService;
        _tokenService = tokenService;
        _dbContext = dbContext;
        _configuration = configuration;
        _logger = loggerFactory.CreateLogger<AuthenticationController>();
    }

    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid) return UnprocessableEntity(loginDto);
        var result =
            await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, loginDto.RememberMe, false);

        if (result != SignInResult.Success) return BadRequest(loginDto);

        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email);

        var generatedToken = _tokenService.BuildToken(_configuration["Jwt:Key"],
            _configuration["Jwt:Issuer"], user);

        if (string.IsNullOrEmpty(generatedToken)) return BadRequest(loginDto);

        HttpContext.Session.SetString("Token", generatedToken);
        return Ok(generatedToken);
    }

    [HttpPost("register")]
    public async Task<ActionResult<string>> Register([FromBody] RegisterDto model)
    {
        if (!ModelState.IsValid) return BadRequest("You did not register successfully");

        var user = new ApplicationUser {UserName = model.Email, Email = model.Email};
        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            return BadRequest("You did not register successfully");
        }

        await _signInManager.SignInAsync(user, false);

        _logger.LogInformation(3, "User account created successfully");

        return Ok();
    }

    [HttpPost("logoff")]
    public async Task<IActionResult> LogOff()
    {
        await _signInManager.SignOutAsync();
        _logger.LogInformation(4, "User logged out");
        return Ok();
    }

    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail(string userId, string code)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null) return NotFound(userId);

        var result = await _userManager.ConfirmEmailAsync(user, code);

        if (result.Succeeded) return Ok();

        return BadRequest("Did not confirm email");
    }

    [HttpPost("forgot-password")]
    public async Task<ActionResult<ForgotPasswordDto>> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
    {
        if (!ModelState.IsValid) return forgotPasswordDto;
        var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);

        Url.Action("ResetPassword", "Authentication",
            new {userId = user.Id}, HttpContext.Request.Scheme);

        await _emailService.SendResetPasswordEmail(forgotPasswordDto.Email);
        return Ok();
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
    {
        if (!ModelState.IsValid) return UnprocessableEntity(resetPasswordDto);

        var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);

        if (user == null) return NotFound(resetPasswordDto);

        var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Code, resetPasswordDto.Password);
        if (result.Succeeded) return Ok();

        return BadRequest(result);
    }
}