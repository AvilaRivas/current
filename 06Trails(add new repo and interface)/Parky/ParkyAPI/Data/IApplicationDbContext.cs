using Microsoft.EntityFrameworkCore;
using ParkyAPI.Models;

namespace ParkyAPI.Data
{
    public interface IApplicationDbContext
    {
        DbSet<T> Set<T>() where T : class;

        int SaveChanges();
    }
}