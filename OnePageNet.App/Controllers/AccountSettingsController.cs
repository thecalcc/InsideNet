using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnePageNet.Data.Data.Entities;
using OnePageNet.Data.Data.Models;

namespace OnePageNet.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountSettingsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountSettingsController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<AccountSettingsDTO> GetSettingsById([FromRoute] string id)
        {
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
        [Route("update")]
        public async Task<IActionResult> UpdateEntity([FromRoute] string id, [FromBody] AccountSettingsDTO dto)
        {
            // if (id != dto.Id) return BadRequest();
            //
            // _userService.Update(dto);
            //
            // try
            // {
            //     await _userService.SaveChangesAsync();
            // }
            // catch (DbUpdateConcurrencyException)
            // {
            //     if (!_userService.Exists(id)) return NotFound();
            //     throw new Exception("NPC doesn't exist");
            // }

            return NoContent();
        }
    }
}