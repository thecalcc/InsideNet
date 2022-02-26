using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnePageNet.App.Data.Entities;
using OnePageNet.App.Data.Models;
using OnePageNet.App.Services.Interfaces;

namespace OnePageNet.App.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : Controller
{
    private readonly ILogger _logger;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IEmailService _emailService;
    private readonly UserManager<ApplicationUser> _userManager;

    public AccountController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IEmailService emailService,
        ILoggerFactory loggerFactory)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _emailService = emailService;
        _logger = loggerFactory.CreateLogger<AccountController>();
    }

    [Route("login")]
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid) return UnprocessableEntity(loginDto);
        var result =
            await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, loginDto.RememberMe, false);

        if (result.Succeeded)
        {
            _logger.LogInformation(1, "User logged in");
            return Ok();
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return BadRequest(loginDto);

    }

    // [Route("register/{email}/{password}/{confirmPassword}")]
    // [HttpGet]
    // public async Task<ActionResult<string>> Register([FromRoute] string email, [FromRoute] string password,
    //     [FromRoute] string repeatPass)
    // {
    //     Console.WriteLine("work ni");
    //
    //     return default;
    // }
    
    [Route("register")]
    [HttpPost]
    public async Task<ActionResult<string>> Register([FromBody] RegisterDto model)
    {
        if (!ModelState.IsValid) return BadRequest("You did not register successfully");
        
        var user = new ApplicationUser {UserName = model.Email, Email = model.Email};
        var result = await _userManager.CreateAsync(user, model.Password);
        
        if (!result.Succeeded)
        {
            AddErrors(result);
            return BadRequest("You did not register successfully");
        }

        // TODO Front end has to send us a RememberMe param -> isPersistent
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

        Url.Action("ResetPassword", "Account",
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

        AddErrors(result);
        return BadRequest(result);
    }

    private void AddErrors(IdentityResult result)
    {
        foreach (var error in result.Errors) ModelState.AddModelError(string.Empty, error.Description);
    }
}