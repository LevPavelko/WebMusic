
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMusic.DAL.Interfaces
{
    public interface IRepository <T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task <T> Get(int id);
        Task <T> GetByName(string name);
        Task<List<T>> Search(string name);
        Task Create(T item);
        void Update(T item);
        Task Delete(int id);
    }
}
