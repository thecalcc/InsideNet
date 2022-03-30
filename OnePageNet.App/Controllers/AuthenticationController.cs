using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnePageNet.Data.Data.Entities;
using OnePageNet.Data.Data.Models;
using OnePageNet.Services.Services.Interfaces;
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
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthenticationController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IEmailService emailService,
        ITokenService tokenService,
        IUserService userService,
        IConfiguration configuration,
        ILoggerFactory loggerFactory)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _emailService = emailService;
        _tokenService = tokenService;
        _userService = userService;
        _configuration = configuration;
        _logger = loggerFactory.CreateLogger<AuthenticationController>();
    }

    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid) return UnprocessableEntity(loginDto);

        var user = await _userService.GetByEmail(loginDto.Email);

        var result =
            await _signInManager.PasswordSignInAsync(user.UserName, loginDto.Password, loginDto.RememberMe, false);

        if (result != SignInResult.Success) return BadRequest(loginDto);

        var dto = await _userService.GetByEmail(loginDto.Email);

        if (dto.Id == null) return BadRequest("There's no such user");

        var generatedToken = await _tokenService.BuildToken(_configuration["Jwt:Key"],
            _configuration["Jwt:Issuer"], dto.Id);

        if (string.IsNullOrEmpty(generatedToken)) return BadRequest(loginDto);

        HttpContext.Session.SetString("Token", generatedToken);
        return Ok(new {generatedToken, user.Id});
    }

    [HttpPost("register")]
    public async Task<ActionResult<string>> Register([FromBody] RegisterDto registerDto)
    {
        if (!ModelState.IsValid) return BadRequest("You did not register successfully");

        var user = new ApplicationUser
        {
            UserName = registerDto.Username,
            Email = registerDto.Email,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            DoB = registerDto.DoB,
            MediaURI = registerDto.MediaURI,
            PhoneNumber = registerDto.PhoneNumber,
            Gender = registerDto.Gender
        };

        await _userManager.CreateAsync(user, registerDto.Password);

        var registeredUser = await _userService.GetByEmail(registerDto.Email);

        if (string.IsNullOrEmpty(registeredUser.Id)) return BadRequest("Something went wrong on our end.");

        var generatedToken = await _tokenService.BuildToken(_configuration["Jwt:Key"],
            _configuration["Jwt:Issuer"], registeredUser.Id);

        if (string.IsNullOrEmpty(generatedToken)) return BadRequest(registerDto);

        await _signInManager.SignInAsync(user, false);

        _logger.LogInformation(3, "User account created successfully");

        HttpContext.Session.SetString("Token", generatedToken);
        return Ok(new {generatedToken, user.Id});
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