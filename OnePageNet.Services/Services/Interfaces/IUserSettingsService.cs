using OnePageNet.Data.Data.Entities;

namespace OnePageNet.Services.Services.Interfaces;

public interface IUserSettingsService
{
    Task<bool> UpdateUsername(string userId, string userName);
    Task<bool> UpdateSettings(UpdateSettingsDTO dto);
    Task<bool> UpdateEmail(string userId, string newEmail);
    Task<bool> UpdatePassword(string userId, string oldPassword, string newPassword);
}