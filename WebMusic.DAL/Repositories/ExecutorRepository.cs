using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebMusic.DAL.EF;
using WebMusic.DAL.Entities;
using WebMusic.DAL.Interfaces;

namespace WebMusic.DAL.Repositories
{
    public class ExecutorRepository : IRepository<Executor>
    {
        private WebMusicContext db;

        public ExecutorRepository(WebMusicContext context)
        {
            this.db = context;
        }
        public async Task<IEnumerable<Executor>> GetAll()
        {
            return await db.executor.ToListAsync();
        }
        public async Task Create(Executor executor)
        {
            await db.executor.AddAsync(executor);
        }

        public void Update(Executor executor)
        {
            db.Entry(executor).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            Executor? executor = await db.executor.FindAsync(id);
            if (executor != null)
                db.executor.Remove(executor);
        }

        
        public async Task<Executor> Get(int id)
        {
            Executor? executor = await db.executor.FirstOrDefaultAsync(g => g.Id == id);
            return executor;
        }
        public async Task<Executor> GetByName(string name)
        {
            Executor? executor = await db.executor.FirstOrDefaultAsync(g => g.Name == name);
            return executor;
        }
    }
}
