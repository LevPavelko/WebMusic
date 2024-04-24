using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMusic.DAL.EF;
using WebMusic.DAL.Interfaces;
using WebMusic.DAL.Entities;
using System.Numerics;


namespace WebMusic.DAL.Repositories 
{
    
    public class EFUnitOfWork : IUnitOfWork
    {
        private WebMusicContext db;
        private UserRepository userRepository;
        private MediaRepository mediaRepository;
        private GenreRepository genreRepository;
        private ExecutorRepository executorRepository;
        public EFUnitOfWork(WebMusicContext context)
        {
            db = context;
        }

        public IUserRepository user
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public IMediaRepository media
        {
            get
            {
                if (mediaRepository == null)
                    mediaRepository = new MediaRepository(db);
                return mediaRepository;
            }
        }
        public IRepository <Genre> genre
        {
            get
            {
                if (genreRepository == null)
                    genreRepository = new GenreRepository(db);
                return genreRepository;
            }
        }

        public IRepository<Executor> executor
        {
            get
            {
                if (executorRepository == null)
                    executorRepository = new ExecutorRepository(db);
                return executorRepository;
            }
        }


        public async Task Save()
        {
            await db.SaveChangesAsync();
        }
    }
    
}
