using ParkyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Repository.IRepository
{
    public interface IUserRepository<T> : IBaseRepository<T>
    {
        bool IsUniqueUser(string username);
        T Authenthicate(string username, string password);
        User Register(string username, string password);
    }
}
