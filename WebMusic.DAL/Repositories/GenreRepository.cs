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
    public class GenreRepository : IRepository<Genre>
    {
        private WebMusicContext db;

        public GenreRepository(WebMusicContext context)
        {
            this.db = context;
        }
        public async Task<IEnumerable<Genre>> GetAll()
        {
            return await db.genre.ToListAsync();
        }
        public async Task Create(Genre genre)
        {
            await db.genre.AddAsync(genre);
        }

        public void Update(Genre genre)
        {
            db.Entry(genre).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            Genre? genre = await db.genre.FindAsync(id);
            if (genre != null)
                db.genre.Remove(genre);
        }
        public async Task<Genre> Get(int id)
        {
            Genre? genre = await db.genre.FirstOrDefaultAsync(g => g.Id == id);
            return genre;
        }
        public async Task<Genre> GetByName(string name)
        {
            Genre? genre = await db.genre.FirstOrDefaultAsync(g => g.Name == name);
            return genre;
        }
        public async Task<List<Genre>> Search(string name)
        {
            return await db.genre.Where(e => e.Name.Contains(name)).ToListAsync();

        }
    }
}
