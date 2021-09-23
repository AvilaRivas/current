using ParkyAPI.Data;
using ParkyAPI.Repository.IRepository;
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

        public IEnumerable<T> GetAll()
        {
            return this._db.Set<T>().ToList();
        }

        public T Get(int nationalParkId)
        {
            return this._db.Set<T>().Find(nationalParkId);
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> expression)
        {
            return this._db.Set<T>().Where(expression);
        }
        public bool Create(T nationalPark)
        {
            this._db.Set<T>().Add(nationalPark);
            return Save();
        }

        public bool Update(T nationalPark)
        {
            this._db.Set<T>().Update(nationalPark);
            return Save();
        }

        public bool Delete(T nationalPark)
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
