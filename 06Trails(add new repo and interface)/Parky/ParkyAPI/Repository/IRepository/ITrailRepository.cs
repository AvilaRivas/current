using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ParkyAPI.Repository.IRepository
{
    public interface ITrailRepository<T> : IBaseRepository<T>
    {
        ICollection<T> GetTrailsInNationalPark(int npId);
        T GetWithNationalPark(int trailId);
        IEnumerable<T> GetAllWithNationalPark();
    }
}
