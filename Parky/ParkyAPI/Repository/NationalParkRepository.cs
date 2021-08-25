using ParkyAPI.Data;
using ParkyAPI.Models;
using ParkyAPI.Repository.INationalParkRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ParkyAPI.Repository
{
    public class NationalParkRepository<T> :BaseRepository<T>,  INationalParkRepository<T> where T : NationalPark
    {

        public NationalParkRepository(ApplicationDbContext db)
            : base(db)
        {

        }

        public bool NationalParkExists(string name)
        {
            return this._db.Set<T>().Any(x => x.Name.ToLower().Trim() == name.ToLower().Trim());
        }

        public bool NationalParkExists(int id)
        {
            return this._db.Set<T>().Any(x => x.Id == id);
        }
    }
}
