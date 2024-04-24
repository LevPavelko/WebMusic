using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebMusic.DAL.EF;
using WebMusic.DAL.Entities;
using WebMusic.DAL.Interfaces;

namespace WebMusic.DAL.Repositories
{
    public class FavoriteSongsRepository : IFavSongsRepository
    {
        private WebMusicContext db;
        public FavoriteSongsRepository(WebMusicContext context)
        {
            this.db = context;
        }
        public async Task Create(FavSongs favSong)
        {
            await db.favSongs.AddAsync(favSong);
        }
        public async Task<List<FavSongs>> GetSongsByUser(int id)
        {
            return await db.favSongs.Where(e => e.id_User == id).ToListAsync();
        }
        public async Task Delete(int id)
        {
            FavSongs? song = await db.favSongs.FindAsync(id);
            if (song != null)
                db.favSongs.Remove(song);
        }
        public async Task<IEnumerable<FavSongs>> GetAll()
        {
            return await db.favSongs.ToListAsync();
        }
    }
}
