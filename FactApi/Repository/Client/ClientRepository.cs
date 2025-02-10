// add user repository

using FactApi.Domain.Models;
using FactApi.Domain.ViewModels;
using Microsoft.EntityFrameworkCore;

public interface IClientRepository
{
   
    Task<Client> GetById(int id);

    Task<Client> Register(Client client);

    Task<IEnumerable<Client>> ToList();

    Task<bool> SaveChanges();

}

/// <summary>
/// Repository for managing user data.
/// </summary>
public class ClientRepository : IClientRepository
{
    private readonly AppDbContext context;

    public ClientRepository(AppDbContext _context)
    {
        this.context = _context;
    }

    public async Task<Client> GetById(int id)
    {
        return await context.Clients.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Client>> ToList()
    {
        return await context.Clients.Include(x => x.Profession).ToArrayAsync();
    }

    public async Task<Client> Register(Client client)
    {
        context.Clients.Add(client);
        return client;
    }

    public async Task<bool> SaveChanges()
    {
        return await context.SaveChangesAsync() > 0;
    }
}