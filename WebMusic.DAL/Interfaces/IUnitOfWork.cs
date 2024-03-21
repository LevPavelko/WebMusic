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
        IRepository<Users> user { get; }
        IRepository<Media> media { get; }

        IRepository<Genre> genre { get; }
        IRepository<Executor> executor { get; }

        Task Save();
    }
}

