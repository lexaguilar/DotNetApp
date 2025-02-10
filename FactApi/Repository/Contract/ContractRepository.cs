// add user repository

using FactApi.Domain.Models;
using FactApi.Domain.ViewModels;
using Microsoft.EntityFrameworkCore;

public interface IContractRepository
{
   
    Task<Contract> GetById(int id);

    Task<IEnumerable<Contract>> GetList();

    Task<Contract> GetByClient(int clientId);

    Task<Contract> Register(Contract contract);

    Task<bool> SaveChanges();
    
}

/// <summary>
/// Repository for managing user data.
/// </summary>
public class ContractRepository : IContractRepository
{
    private readonly AppDbContext context;

    public ContractRepository(AppDbContext _context)
    {
        this.context = _context;
    }

    public async Task<Contract> GetByClient(int clientId)
    {
        var result = await context.Contracts
        .Include(x => x.Client)
        .FirstOrDefaultAsync(x => x.ClientId == clientId);

        return result;
    }

    public async Task<Contract> GetById(int id)
    {
        return await context.Contracts
        .Include(x => x.Client)
        .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Contract>> GetList()
    {
        var result = await context.Contracts
        .Include(x => x.Client)
        .ToArrayAsync();

        return result;
    }

    public async Task<Contract> Register(Contract contract)
    {
        await context.Contracts.AddAsync(contract);
        return contract;
    }

    public async Task<bool> SaveChanges()
    {
        return await context.SaveChangesAsync() > 0;
    }
}