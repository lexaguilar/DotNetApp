// add user repository

using FactApi.Domain.Models;
using FactApi.Domain.ViewModels;
using Microsoft.EntityFrameworkCore;

public interface IBillRepository
{

    Task<Bill> GetById(int id);

    IQueryable<Bill> GetList();

    Task<bool> SaveChanges();

}

/// <summary>
/// Repository for managing user data.
/// </summary>
public class BillRepository : IBillRepository
{
    private readonly AppDbContext context;

    public BillRepository(AppDbContext _context)
    {
        this.context = _context;
    }

    public async Task<Bill> GetById(int id)
    {
        return await context.Bills.FirstOrDefaultAsync(x => x.Id == id);
    }

    public IQueryable<Bill> GetList()
    {
        return context.Bills
        .Include(x => x.Client)
        .ThenInclude(x => x.Profession)
        .OrderByDescending(x => x.Id).AsQueryable();
    }

    public async Task<bool> SaveChanges()
    {
        return await context.SaveChangesAsync() > 0;
    }
}