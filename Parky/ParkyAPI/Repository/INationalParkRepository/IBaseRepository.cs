using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ParkyAPI.Repository.INationalParkRepository
{
    public interface IBaseRepository<T>
    {
        IEnumerable<T> GetNatiolanParks();
        T GetNationalPark(int nationalParkId);
        IQueryable<T> Find(Expression<Func<T, bool>> expression);
        bool CreateNationalPark(T nationalPark);
        bool UpdateNationalPark(T nationalPark);
        bool DeleteNationalPark(T nationalPark);
        bool Save();
    }
}
