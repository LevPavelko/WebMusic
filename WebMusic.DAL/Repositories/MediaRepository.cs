using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebMusic.DAL.Entities;
using WebMusic.DAL.Interfaces;
using WebMusic.DAL.EF;

namespace WebMusic.DAL.Repositories
{
    public class MediaRepository : IRepository<Media>
    {
        private WebMusicContext db;

        public MediaRepository(WebMusicContext context)
        {
            this.db = context;
        }
        public async Task<IEnumerable<Media>> GetAll()
        {
            return await db.media.ToListAsync();
        }
        public async Task Create(Media media)
        {
            await db.media.AddAsync(media);
        }

        public void Update(Media media)
        {
            db.Entry(media).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            Media? media = await db.media.FindAsync(id);
            if (media != null)
                db.media.Remove(media);
        }
        public async Task<Media> Get(int id)
        {
            Media? media = await db.media.FirstOrDefaultAsync(m=> m.Id ==  id);
            return media;
        }
        public async Task<Media> GetByName(string name)
        {
            Media? media = await db.media.FirstOrDefaultAsync(g => g.Title == name);
            return media;
        }
    }
}
