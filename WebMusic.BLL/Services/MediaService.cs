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
using System.Numerics;

namespace WebMusic.BLL.Services
{
    public class MediaService : IMediaService
    {
        
        IUnitOfWork Database { get; set; }
        public MediaService(IUnitOfWork uow)
        {
            
            Database = uow;
        }
        public async Task CreateMedia(MediaDTO mediaDTO)
        {
            var song = new Media
            {
                Id = mediaDTO.Id,
                Title = mediaDTO.Title,
                id_Executor = mediaDTO.id_Executor,
                id_Genre = mediaDTO.id_Genre,
                Path = mediaDTO.Path,
                Id_User = mediaDTO.Id_User


            };
            await Database.media.Create(song);
            await Database.Save();
        }
        public async Task UpdateMedia(MediaDTO mediaDTO)
        {
            var song = new Media
            {
                Id = mediaDTO.Id,
                Title = mediaDTO.Title,
                id_Executor = mediaDTO.id_Executor,
                id_Genre = mediaDTO.id_Genre,
                Path = mediaDTO.Path,
                Id_User = mediaDTO.Id_User


            };
            Database.media.Update(song);
            await Database.Save();
        }
        public async Task DeleteMedia(int id)
        {
            await Database.media.Delete(id);
            await Database.Save();
        }
        public async Task<MediaDTO> GetMedia(int id)
        {
            var song = await Database.media.Get(id);
            if (song == null)
                throw new ValidationException("Wrong song!", "");
            return new MediaDTO
            {

                Id = song.Id,
                Title = song.Title,
                id_Executor = song.id_Executor,
                Executor = song.Executor?.Name,
                id_Genre = song.id_Genre,
                Genre = song.Genre?.Name,
                Path = song.Path,
                Id_User = song.Id_User
            };
        }
        public async Task<MediaDTO> GetMediaByName(string name)
        {
            var song = await Database.media.GetByName(name);
            if (song == null)
                throw new ValidationException("Wrong song!", "");
            return new MediaDTO
            {

                Id = song.Id,
                Title = song.Title,
                id_Executor = song.id_Executor,
                Executor = song.Executor?.Name,
                id_Genre = song.id_Genre,
                Genre = song.Genre?.Name,
                Path = song.Path,
                Id_User = song.Id_User
            };
        }
        public async Task<IEnumerable<MediaDTO>> GetMedias()
        {
         
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Media, MediaDTO>()
                    .ForMember("Executor", opt => opt.MapFrom(src => src.Executor.Name))
                    .ForMember("Genre", opt => opt.MapFrom(src => src.Genre.Name));
            });
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<Media>, IEnumerable<MediaDTO>>(await Database.media.GetAll());
        }
        public async Task<List<MediaDTO>> Searching(string name)
        {
            var song = await Database.media.Search(name);
            if (song == null)
                throw new ValidationException("Wrong executor!", "");
            var mediaDTOs = song.Select(a => new MediaDTO
            {
                Id = a.Id,
                Title = a.Title,
                id_Executor = a.id_Executor,
                Executor = a.Executor?.Name,
                id_Genre = a.id_Genre,
                Genre = a.Genre?.Name,
                Path = a.Path,
                Id_User = a.Id_User

            }).ToList();
          

            return mediaDTOs;

        }
    }
}
