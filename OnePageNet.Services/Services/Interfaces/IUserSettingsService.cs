using OnePageNet.Data.Data.Entities;

namespace OnePageNet.Services.Services.Interfaces;

public interface IUserSettingsService
{
    Task<bool> UpdateSettings(UpdateSettingsDTO dto);
}