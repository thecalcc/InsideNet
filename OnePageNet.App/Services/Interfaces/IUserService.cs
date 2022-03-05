using Hangfire.Annotations;
using OnePageNet.App.Data.Entities;
using OnePageNet.App.Data.Models;

namespace OnePageNet.App.Services.Interfaces;

public interface IUserService
{
    Task<List<UserDto>> GetAll();
    Task<UserDto> GetById(string id);
    Task<UserDto> GetByEmail(string email);
    void Update(UserDto userDto);
    Task SaveChangesAsync();
    bool Exists(string id);
    Task AddAsync(UserDto entity);
    Task<bool> AttachUser(UserDto entity);
    Task<bool> Remove(string id);
}