using Microsoft.EntityFrameworkCore;
using ParkyAPI.Data;
using ParkyAPI.Models;
using ParkyAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Repository
{
    public class TrailRepository<T> : BaseRepository<T>, ITrailRepository<T> where T : Trail
    {
        public TrailRepository(IApplicationDbContext db)
            : base(db)
        {

        }

        public ICollection<T> GetTrailsInNationalPark(int npId)
        {
            return _db.Set<T>().Include(c => c.NationalPark).Where(c => c.NationalParkId == npId).ToList();
        }

        public T GetWithNationalPark(int trailId)
        {
            return this._db.Set<T>().Include(np => np.NationalPark).FirstOrDefault(x => x.Id == trailId);
        }

        public IEnumerable<T> GetAllWithNationalPark()
        {
            return this._db.Set<T>().Include(np => np.NationalPark).ToList();
        }
    }
}
