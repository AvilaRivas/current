using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ParkyAPI.Repository.IRepository
{
    public interface INationalParkRepository<T> : IBaseRepository<T>
    {
        bool NationalParkExists(string name);
        bool NationalParkExists(int id);
    }
}
