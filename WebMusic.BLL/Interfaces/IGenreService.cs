﻿using System;
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
        Task<GenreDTO> GetGenre(int id);
        Task<GenreDTO> GetGenreByName(string name);
        Task<List<GenreDTO>> Searching(string name);
        Task<IEnumerable<GenreDTO>> GetGenres();
    }
}
