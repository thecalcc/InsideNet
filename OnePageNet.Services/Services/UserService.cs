using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnePageNet.Data.Data;
using OnePageNet.Data.Data.Entities;
using OnePageNet.Data.Data.Models;
using OnePageNet.Helpers.Helpers;
using OnePageNet.Services.Services.Interfaces;

namespace OnePageNet.Services.Services;

public class UserService : IUserService
{
    private readonly OnePageNetDbContext _dbContext;
    private readonly IMapper _mapper;

    public UserService(OnePageNetDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<UserDto>> GetAllFriends(string userId)
    {
        return _mapper.Map<List<UserDto>>(
            _dbContext.Users.Where(x =>
                x.CurrentRelationships.Where(x => x.TargetUser.Id == userId)
                    .Any(x => x.UserRelationship.Name == UserRelationConstants.Friends)
            ) ?? throw new Exception("No users found"));
    }

    public async Task<List<UserDto>> GetAll()
    {
        return _mapper.Map<List<UserDto>>(await _dbContext.Users.ToListAsync()) ??
               throw new Exception("No users found");
    }

    public async Task<ApplicationUser> GetUserEntityById(string id)
    {
        return await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == id) ??
               throw new Exception("User with the given id wasn't found");
    }

    public async Task<UserDto> GetById(string id)
    {
        return _mapper.Map<UserDto>(await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == id)) ??
               throw new Exception("User with the given id wasn't found");
    }

    public async Task<UserDto> GetByEmail(string email)
    {
        return _mapper.Map<UserDto>(await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email)) ??
               throw new Exception("User with the given email wasn't found");
    }

    public async void Update(UserDto userDto)
    {
        _dbContext.Entry(_mapper.Map<ApplicationUser>(userDto)).State = EntityState.Modified;
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public bool Exists(string id)
    {
        return _dbContext.Users.Any(e => e.Id.ToString() == id);
    }

    public async Task<bool> AttachUser(UserDto userDto)
    {
        var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == userDto.Id);

        if (user?.Id != userDto.Id || user == null) return false;

        _dbContext.Attach(user);

        return true;
    }

    public async Task<bool> Remove(string id)
    {
        var user = await _dbContext.Users.SingleAsync(x => x.Id == id);
        _dbContext.Users.Remove(user);
        return true;
    }

    public async Task<List<UserDto>> GetFilteredUsers(string search)
    {
        var filteredUsers = _mapper.Map<List<UserDto>>(await _dbContext.Users.Where(x =>
                x.UserName.ToLower().Contains(search.ToLower())
                || x.FirstName.ToLower().Contains(search.ToLower())
                || x.LastName.ToLower().Contains(search.ToLower())
                || x.Email.ToLower().Contains(search.ToLower()))
            .ToListAsync());
        if (filteredUsers.Count > 0) return filteredUsers;
        else throw new Exception("No users match search");
    }
}