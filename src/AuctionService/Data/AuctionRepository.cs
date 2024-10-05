using System.Linq.Expressions;
using AuctionService.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Data;

public interface IAuctionRepository: IDisposable
{
    Task<IEnumerable<Auction>> GetAuctionsAsync(Expression<Func<Auction, bool>> filter);
    Task<Auction> GetAuctionAsync(Guid id);
    Task AddAuctionAsync(Auction auction);
    void UpdateAuction(Auction auction);
    Task DeleteAuctionAsync(Guid id);
    Task SaveAsync();
    Task<IEnumerable<Auction>> ListAsync();
}

public class AuctionRepository(AuctionContext context) : IAuctionRepository, IDisposable
{
    private bool _disposed = false;

    private IQueryable<Auction> Query => context.Auctions.AsQueryable();

    public async Task<IEnumerable<Auction>> ListAsync()
    {
        return await Query.OrderBy(x => x.Item.Make).ToListAsync();
    }
    
    public async Task<IEnumerable<Auction>> GetAuctionsAsync(Expression<Func<Auction, bool>> filter)
    {
        return await Query.Where(filter).OrderBy(x => x.Item.Make).ToListAsync();
    }
    
    public async Task<Auction> GetAuctionAsync(Guid id)
    {
        return await context.Auctions.FindAsync(id);
    }

    public async Task AddAuctionAsync(Auction auction)
    {
         await context.Auctions.AddAsync(auction);
    }

    public void UpdateAuction(Auction auction)
    {
        context.Entry(auction).State = EntityState.Modified;
    }

    public async Task DeleteAuctionAsync(Guid id)
    {
        var auction = await context.Auctions.FindAsync(id);
        if (auction != null) context.Auctions.Remove(auction);
    }

    public async Task SaveAsync()
    {
        await context.SaveChangesAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }
        
        _disposed = true;
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private IQueryable<Auction> CreateAuctionQuery()
    {
        return context.Auctions.AsQueryable();
    }
}