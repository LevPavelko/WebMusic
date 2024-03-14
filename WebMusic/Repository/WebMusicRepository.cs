using Microsoft.EntityFrameworkCore;
using WebMusic.Models;
using WebMusic.Repository;
namespace WebMusic.Repository
{
    public class WebMusicRepository : IRepository
    {
        private readonly WebMusicContext _context;
        public WebMusicRepository(WebMusicContext context)
        {
            _context = context;
        }
        public async Task<List<Media>> GetMediaList()
        {
            return await _context.media.ToListAsync();
        }
        public async Task Create(Media c)
        {
            await _context.media.AddAsync(c);
        }

        
        public void Edit(Media c)
        {
            _context.Entry(c).State = EntityState.Modified;
        }

        public async Task Delete(Media id)
        {
            Media? c = await _context.media.FindAsync(id);
            if (c != null)
                _context.media.Remove(c);
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

    }
}
