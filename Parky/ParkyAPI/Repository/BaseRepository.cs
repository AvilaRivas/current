using ParkyAPI.Data;
using ParkyAPI.Repository.INationalParkRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ParkyAPI.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly IApplicationDbContext _db;

        public BaseRepository(IApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<T> GetNatiolanParks()
        {
            return this._db.Set<T>().ToList();
        }

        public T GetNationalPark(int nationalParkId)
        {
            return this._db.Set<T>().Find(nationalParkId);
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> expression)
        {
            return this._db.Set<T>().Where(expression);
        }
        public bool CreateNationalPark(T nationalPark)
        {
            this._db.Set<T>().Add(nationalPark);
            return Save();
        }

        public bool UpdateNationalPark(T nationalPark)
        {
            this._db.Set<T>().Update(nationalPark);
            return Save();
        }

        public bool DeleteNationalPark(T nationalPark)
        {
            this._db.Set<T>().Remove(nationalPark);
            return Save();
        }

        public bool Save()
        {
            return this._db.SaveChanges() >= 0 ? true : false;
        }
    }
}
