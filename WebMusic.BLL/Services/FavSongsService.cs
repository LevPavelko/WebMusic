using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using WebMusic.BLL.DTO;
using WebMusic.BLL.Interfaces;
using WebMusic.DAL.Entities;
using WebMusic.DAL.Interfaces;

namespace WebMusic.BLL.Services
{
    public class FavSongsService : IFavSongsService
    {
        IUnitOfWork Database { get; set; }
        public FavSongsService(IUnitOfWork uow)
        {

            Database = uow;
        }
        public async Task AddSong(FavSongsDTO favSongsDTO)
        {
            var song = new FavSongs
            {
                Id = favSongsDTO.Id,
                id_User = favSongsDTO.Id_User,
                id_Song = favSongsDTO.Id_Song



            };
            await Database.favSongs.Create(song);
            await Database.Save();
        }
        public async Task DeleteSong(int id)
        {
            await Database.favSongs.Delete(id);
            await Database.Save();
        }
        public async Task<List<FavSongsDTO>> GetSongsByUser(int id)
        {
            var userId = await Database.favSongs.GetSongsByUser(id);
            if (userId == null)
                throw new ValidationException("Wrong id!");
            var favSongsDTO = userId.Select(a => new FavSongsDTO
            {
                Id = a.Id,
                Id_User = a.id_User,
                Id_Song = a.id_Song

            }).ToList();
            return favSongsDTO;
        }
        public async Task<IEnumerable<FavSongsDTO>> GetAllItems()
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FavSongs, FavSongsDTO>()
                    .ForMember("Id_User", opt => opt.MapFrom(src => src.user.Id))
                    .ForMember("Id_Song", opt => opt.MapFrom(src => src.media.Id));
            });
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<FavSongs>, IEnumerable<FavSongsDTO>>(await Database.favSongs.GetAll());
        }
    }
}
