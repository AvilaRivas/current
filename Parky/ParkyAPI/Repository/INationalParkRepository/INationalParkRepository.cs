using ParkyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ParkyAPI.Repository.INationalParkRepository
{
    public interface INationalParkRepository<T> : IBaseRepository<T>
    {
        bool NationalParkExists(string name);
        bool NationalParkExists(int id);
    }
}
