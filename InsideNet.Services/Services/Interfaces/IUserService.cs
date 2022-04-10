using InsideNet.Data.Data.Entities;
using InsideNet.Data.Data.Models;

namespace InsideNet.Services.Services.Interfaces;

public interface IUserService
{
    Task<List<UserDto>> GetAllFriends(string userId);
    Task<List<UserDto>> GetAll();
    Task<UserDto> GetById(string id);
    Task<UserDto> GetByEmail(string email);
    void Update(UserDto userDto);
    Task SaveChangesAsync();
    bool Exists(string id);
    Task<ApplicationUser> GetUserEntityById(string id);
    Task<bool> AttachUser(UserDto userDto);
    Task<bool> RemoveAsync(string id);
    Task<List<UserDto>> GetFilteredUsers(string search);
}