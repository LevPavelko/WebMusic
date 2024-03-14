
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

        Task Create(T item);
        void Update(T item);
        Task Delete(int id);
    }
}
