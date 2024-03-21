using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using WebMusic.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebMusic.DAL.EF
{
    public class WebMusicContext : DbContext
    {
        public DbSet<Users> users { get; set; }
        public DbSet<Media> media { get; set; }
        public DbSet<Genre> genre { get; set; }
        public DbSet<Executor> executor { get; set; }

        public WebMusicContext(DbContextOptions<WebMusicContext> options)
           : base(options)
        {
            Database.EnsureCreated();
            //if (Database.EnsureCreated())
            //{
            //    media?.Add(new Media { Title = "Богдан", id_Executor = 1, id_Genre = 2, Id_User = 1 });

            //    SaveChanges();
            //}
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Media>()
                .HasOne(m => m.User)
                .WithMany(u => u.media)
                .HasForeignKey(m => m.Id_User)
                .IsRequired();

            modelBuilder.Entity<Media>()
                .HasOne(m => m.Genre)
                .WithMany(u => u.media)
                .HasForeignKey(m => m.id_Genre)
                .IsRequired();

            modelBuilder.Entity<Media>()
               .HasOne(m => m.Executor)
               .WithMany(u => u.media)
               .HasForeignKey(m => m.id_Executor)
               .IsRequired();


        }
    }
}
