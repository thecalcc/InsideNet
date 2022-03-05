using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnePageNet.App.Data;
using OnePageNet.App.Data.Entities;
using OnePageNet.App.Data.Models;
using OnePageNet.App.Services.Interfaces;

namespace OnePageNet.App.Services;

public class UserService : IUserService
{
    private readonly OnePageNetDbContext _dbContext;
    private readonly IMapper _mapper;

    public UserService(OnePageNetDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<UserDto>> GetAll()
    {
        return _mapper.Map<List<UserDto>>(await _dbContext.Users.ToListAsync()) ??
               throw new Exception("No users found");
    }

    public async Task<UserDto> GetById(string id)
    {
        return _mapper.Map<UserDto>(await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == id)) ??
               throw new Exception("User with the given email wasn't found");
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

    public async Task AddAsync(UserDto userDto)
    {
        // TODO propaby fix -> first fetch 
        await _dbContext.Users.AddAsync(_mapper.Map<ApplicationUser>(userDto));
        await _dbContext.SaveChangesAsync();
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
}