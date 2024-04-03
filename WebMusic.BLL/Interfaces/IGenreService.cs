using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMusic.BLL.DTO;

namespace WebMusic.BLL.Interfaces
{
    public interface IGenreService
    {
        Task CreateGenre(GenreDTO genreDTO);
        Task UpdateGenre(GenreDTO genreDTO);
        Task DeleteGenre(int id);
        Task<GenreDTO> GetGenre(string name);
        Task<IEnumerable<GenreDTO>> GetGenres();
    }
}
