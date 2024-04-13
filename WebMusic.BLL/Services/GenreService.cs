using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMusic.BLL.DTO;
using WebMusic.BLL.Interfaces;
using WebMusic.DAL.Entities;
using WebMusic.DAL.Interfaces;
using WebMusic.BLL.Infrastructure;
using AutoMapper;

namespace WebMusic.BLL.Services
{
    public class GenreService : IGenreService
    {
        IUnitOfWork Database { get; set; }
        public GenreService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public async Task CreateGenre(GenreDTO genreDTO)
        {
            var genre = new Genre
            {
                Id = genreDTO.Id,
                Name = genreDTO.Name

            };
            await Database.genre.Create(genre);
            await Database.Save();
        }
        public async Task UpdateGenre(GenreDTO genreDTO)
        {
            var genre = new Genre
            {
                Id = genreDTO.Id,
                Name = genreDTO.Name

            };
            Database.genre.Update(genre);
            await Database.Save();
        }
        public async Task DeleteGenre(int id)
        {
            await Database.genre.Delete(id);
            await Database.Save();
        }
        public async Task<GenreDTO> GetGenre(int id)
        {
            var genre = await Database.genre.Get(id);
            if (genre == null)
                throw new ValidationException("Wrong genre!", "");
            return new GenreDTO
            {
                Id = genre.Id,
                Name = genre.Name

            };
        }
        public async Task<GenreDTO> GetGenreByName(string name)
        {
            var genre = await Database.genre.GetByName(name);
            if (genre == null)
                throw new ValidationException("Wrong genre!", "");
            return new GenreDTO
            {
                Id = genre.Id,
                Name = genre.Name

            };
        }
        public async Task<IEnumerable<GenreDTO>> GetGenres()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Genre, GenreDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Genre>, IEnumerable<GenreDTO>>(await Database.genre.GetAll());
        }
    }
}
