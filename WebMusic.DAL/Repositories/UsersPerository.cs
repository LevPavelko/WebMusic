using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using WebMusic.DAL.EF;
using WebMusic.DAL.Entities;
using WebMusic.DAL.Interfaces;
using System.Numerics;


namespace WebMusic.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private WebMusicContext db;

        public UserRepository(WebMusicContext context)
        {
            this.db = context;
        }
        public async Task<IEnumerable<Users>> GetAll()
        {
            return await db.users.ToListAsync();
        }
        public async Task Create(Users user)
        {
            await db.users.AddAsync(user);
        }

        public void Update(Users user)
        {
            db.Entry(user).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            Users? user = await db.users.FindAsync(id);
            if (user != null)
                db.users.Remove(user);
        }
        public async Task<Users> Get(int id)
        {
            Users? user = await db.users.FindAsync(id);
            return user;
        }
    }
}
