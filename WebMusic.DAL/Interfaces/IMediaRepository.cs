using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMusic.DAL.Entities;

namespace WebMusic.DAL.Interfaces
{
    public interface IMediaRepository
    {
        Task<IEnumerable<Media>> GetAll();
        Task<Media> Get(int id);
        Task<Media> GetByName(string name);
        Task<List<Media>> Search(string name);
        Task<List<Media>> GetSongsByExecutor(int id);
        Task<List<Media>> GetSongsByGenre(int id);
        Task Create(Media item);
        void Update(Media item);
        Task Delete(int id);
    }
}
