using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMusic.DAL.Entities
{
    public class Media
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual Executor Executor { get; set; }
        public int id_Executor { get; set; }
        public virtual Genre Genre { get; set; }
        public int id_Genre { get; set; }
        public string Path { get; set; }

        public virtual Users User { get; set; }
        public int Id_User { get; set; }

        public virtual ICollection<FavSongs> favSongs { get; set; }
    }
}
