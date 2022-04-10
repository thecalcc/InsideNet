using InsideNet.Data.Data.Entities;
using InsideNet.Data.Data.Models;

namespace InsideNet.Services.Services.Interfaces;

public interface IUserSettingsService
{
    Task<bool> UpdateSettings(UpdateSettingsDto dto);
}