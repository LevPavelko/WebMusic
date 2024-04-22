using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMusic.BLL.DTO;

namespace WebMusic.BLL.Interfaces
{
    public interface IMediaService
    {
        Task CreateMedia(MediaDTO mediaDTO);
        Task UpdateMedia(MediaDTO mediaDTO);
        Task DeleteMedia(int id);
        Task<MediaDTO> GetMedia(int id);
        Task<MediaDTO> GetMediaByName(string name);
        Task<List<MediaDTO>> Searching(string name);
        Task<IEnumerable<MediaDTO>> GetMedias();
    }
}
