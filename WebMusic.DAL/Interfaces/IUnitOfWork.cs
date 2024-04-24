using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WebMusic.DAL.Entities;

namespace WebMusic.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository user { get; }
        IMediaRepository media { get; }
        IFavSongsRepository favSongs { get; }
        IRepository<Genre> genre { get; }
        IRepository<Executor> executor { get; }

        Task Save();
    }
}

