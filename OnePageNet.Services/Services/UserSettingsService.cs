using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnePageNet.Data.Data;
using OnePageNet.Data.Data.Entities;
using OnePageNet.Services.Services.Interfaces;

namespace OnePageNet.Services.Services;

public class UserSettingsService : IUserSettingsService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly OnePageNetDbContext _dbContext;

    public UserSettingsService(UserManager<ApplicationUser> userManager, OnePageNetDbContext dbContext)
    {
        _userManager = userManager;
        _dbContext = dbContext;
    }

    public async Task<bool> UpdateSettings(UpdateSettingsDTO dto)
    {
        try
        {
            var user = await FetchUser(dto.Id);

            if (user.UserName != dto.Username)
            {
                await UpdateUsername(user, dto.Username);
            }

            if (user.Email != dto.Email)
            {
                await UpdateEmail(user, dto.Email);
            }

            if (!VerifyPassword(user, dto.NewPassword) &&
                dto.OldPassword != "********")
            {
                if (dto.NewPassword != "********")
                {
                    await UpdatePassword(user, dto.OldPassword, dto.NewPassword);
                }
            }

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    private bool VerifyPassword(ApplicationUser user, string newPassword)
    {
        return _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, newPassword) ==
               PasswordVerificationResult.Success;
    }

    private async Task<bool> UpdateUsername(ApplicationUser user, string userName)
    {
        var updated = await _userManager.SetUserNameAsync(user, userName);

        return updated.Succeeded;
    }

    private async Task<ApplicationUser> FetchUser(string userId)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
        return user ?? throw new NullReferenceException("User not found");
    }

    private async Task<bool> UpdatePassword(ApplicationUser user, string oldPassword, string newPassword)
    {
        var changed = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        return changed.Succeeded;
    }

    private async Task<bool> UpdateEmail(ApplicationUser user, string newEmail)
    {
        //TODO Token should be sent to the old email through SMTP and the user should input it in a new endpoint call
        // only if the token is valid should the email be changed
        var token = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
        var changed = await _userManager.ChangeEmailAsync(user, newEmail, token);

        return changed.Succeeded;
    }
}