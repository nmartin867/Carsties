using AuctionService.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Data;

public class AuctionContext: DbContext
{
    public DbSet<Auction> Auctions { get; set; }
}