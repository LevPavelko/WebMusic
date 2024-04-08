using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMusic.DAL.Entities;
using WebMusic.DAL.Interfaces;

namespace WebMusic.DAL.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<Users>> GetAll();
        Task<Users> Get(int id);
        Task<Users> GetByLogin(string login);
        Task Create(Users item);
        void Update(Users item);
        Task Delete(int id);
    }
}
