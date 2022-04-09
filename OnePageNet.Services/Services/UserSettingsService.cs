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
        var passUpdate = await UpdatePassword(dto.Id, dto.OldPassword, dto.NewPassword);
        var emailUpdate = await UpdateEmail(dto.Id, dto.Email);
        var updateUsername = await UpdateUsername(dto.Id, dto.Username);

        return passUpdate && emailUpdate && updateUsername;
    }

    public async Task<bool> UpdateUsername(string userId, string userName)
    {
        var user = await FetchUser(userId);
        var updated = await _userManager.SetUserNameAsync(user, userName);

        return updated.Succeeded;
    }

    private async Task<ApplicationUser> FetchUser(string userId)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
        return user ?? throw new NullReferenceException("User not found");
    }

    public async Task<bool> UpdatePassword(string userId, string oldPassword, string newPassword)
    {
        var user = await FetchUser(userId);

        var changed = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        return changed.Succeeded;
    }

    public async Task<bool> UpdateEmail(string userId, string newEmail)
    {
        var user = await FetchUser(userId);

        //TODO Token should be sent to the old email through SMTP and the user should input it in a new endpoint call
        // only if the token is valid should the email be changed
        var token = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
        var changed = await _userManager.ChangeEmailAsync(user, newEmail, token);

        return changed.Succeeded;
    }
}