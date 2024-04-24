using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMusic.DAL.Entities;

namespace WebMusic.DAL.Interfaces
{
    public interface IFavSongsRepository
    {
        Task<IEnumerable<FavSongs>> GetAll();
        Task Create(FavSongs item);
        Task<List<FavSongs>> GetSongsByUser(int id);
        Task Delete(int id);
    }
}
