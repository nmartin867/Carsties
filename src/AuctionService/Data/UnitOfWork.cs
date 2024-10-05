using Microsoft.EntityFrameworkCore;

namespace AuctionService.Data;

public class UnitOfWork<TEntity>(DbContext context, GenericRepository<TEntity> repository) : IDisposable
    where TEntity : class
{
    private bool _disposed;
    
      
    
    public void Save()
    {
        context.SaveChanges();
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
}