using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ParkyAPI.Repository.IRepository
{
    public interface IBaseRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(int nationalParkId);
        IQueryable<T> Find(Expression<Func<T, bool>> expression);
        bool Create(T nationalPark);
        bool Update(T nationalPark);
        bool Delete(T nationalPark);
        bool Save();
    }
}
