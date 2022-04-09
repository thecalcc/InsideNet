using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnePageNet.Data.Data.Entities;
using OnePageNet.Data.Data.Models;
using OnePageNet.Services.Services.Interfaces;

namespace OnePageNet.App.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountSettingsController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserSettingsService _userSettingsService;

    public AccountSettingsController(UserManager<ApplicationUser> userManager,
        IUserSettingsService userSettingsService)
    {
        _userManager = userManager;
        _userSettingsService = userSettingsService;
    }

    [HttpGet]
    [Route("get/{id}")]
    public async Task<AccountSettingsDTO> GetSettingsById([FromRoute] string id)
    {
        // TODO Refactor
        var user = await _userManager.FindByIdAsync(id) ?? throw new Exception("Do you even have an account, bro?");
        return new AccountSettingsDTO
        {
            Id = id,
            Email = user.Email,
            UserName = user.UserName,
            CreatedAt = DateTime.Now
        };
    }

    [HttpPut]
    [Route("update/{userId}")]
    public async Task<ActionResult<bool>> UpdateEntity([FromRoute] string userId, [FromBody] UpdateSettingsDTO dto)
    {
        if (userId != dto.Id) return BadRequest();

        try
        {
            return Ok(await _userSettingsService.UpdateSettings(dto));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}