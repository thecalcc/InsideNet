using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnePageNet.Data.Data.Entities;
using OnePageNet.Data.Data.Models;
using OnePageNet.Services.Services.Interfaces;

namespace OnePageNet.App.Controllers
{
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
        [Route("password-update/{userId}")]
        public async Task<ActionResult<bool>> UpdatePassword([FromRoute] string userId,
            [FromBody] UpdatePasswordDto dto)
        {
            try
            {
                return Ok(await _userSettingsService.UpdatePassword(userId, dto.OldPassword, dto.NewPassword));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("email-update/{userId}")]
        public async Task<ActionResult<bool>> UpdateEmail([FromRoute] string userId,
            [FromBody] string newEmail)
        {
            try
            {
                return Ok(await _userSettingsService.UpdateEmail(userId, newEmail));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("username-update/{userId}")]
        public async Task<ActionResult<bool>> UpdateUsername([FromRoute] string userId,
            [FromBody] string userName)
        {
            try
            {
                return Ok(await _userSettingsService.UpdateUsername(userId, userName));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("full-update")]
        public async Task<ActionResult<bool>> UpdateEntity([FromRoute] string id, [FromBody] UpdateSettingsDTO dto)
        {
            if (id != dto.Id) return BadRequest();

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
}