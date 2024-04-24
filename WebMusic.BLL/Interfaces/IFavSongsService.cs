using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMusic.BLL.DTO;

namespace WebMusic.BLL.Interfaces
{
    public interface IFavSongsService
    {
        Task<IEnumerable<FavSongsDTO>> GetAllItems();
        Task<List<FavSongsDTO>> GetSongsByUser(int id);
        Task AddSong(FavSongsDTO mediaDTO);
        Task DeleteSong(int id);
    }
}
