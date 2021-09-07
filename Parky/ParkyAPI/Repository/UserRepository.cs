using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ParkyAPI.Data;
using ParkyAPI.Models;
using ParkyAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ParkyAPI.Repository
{
    public class UserRepository<T> : BaseRepository<T>, IUserRepository<T> where T : User
    {
        private readonly AppSettings _appSettings;

        public UserRepository(IApplicationDbContext db, IOptions<AppSettings> appsettings) 
            : base(db)
        {
            this._appSettings = appsettings.Value;
        }

        public bool IsUniqueUser(string username)
        {
            var user = _db.Set<T>().SingleOrDefault(x => x.Username == username);
            if (user == null) return true;
            return false;
        }

        public T Authenthicate(string username, string password)
        {
            var user = _db.Set<T>().SingleOrDefault(x => x.Username == username && x.Password == password);
            if (user == null) return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            user.Password = "";
            return user;
        }
        
        public User Register(string username, string password)
        {
            //var test = (T)Activator.CreateInstance(typeof(T), new object[] { T. });

            User userObj = new User()
            {
                Username = username,
                Password = password,
                Role = "Admin"
            };
            Create((T)userObj);
            _db.SaveChanges();
            userObj.Password = "";
            return userObj;
        }
    }
}
