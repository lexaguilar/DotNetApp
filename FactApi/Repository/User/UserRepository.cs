// add user repository

using FactApi.Domain.Models;
using FactApi.Domain.ViewModels;
using Microsoft.EntityFrameworkCore;

public interface IUserRepository
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="username">User nickname.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    Task<User> GetUser(string username);

    Task<User> GetUserByEmail(string email);

    Task<User> Register(User user);
    Task<bool> UserExists(string username);

    Task<bool> EmailExists(string email);

    Task<bool> SaveChanges();
    
}

/// <summary>
/// Repository for managing user data.
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly AppDbContext context;

    public UserRepository(AppDbContext _context)
    {
        this.context = _context;
    }

    public async Task<User> GetUser(string username)
    {
        return await context.Users.FirstOrDefaultAsync(x => x.Username == username);
    }

    public async Task<User> GetUserByEmail(string email)
    {
        throw new NotImplementedException();
        //return await context.Users.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<User> Register(User user)
    {
        await context.Users.AddAsync(user);
        return user;
    }

    public async Task<bool> UserExists(string username)
    {
        return await context.Users.AnyAsync(x => x.Username == username);
    }

    public async Task<bool> EmailExists(string email)
    {
        throw new NotImplementedException();
        //return await _context.Users.AnyAsync(x => x.Email == email);
    }

    public async Task<bool> SaveChanges()
    {
        return await context.SaveChangesAsync() > 0;
    }
}