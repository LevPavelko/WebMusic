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
        public Executor Executor { get; set; }
        public int id_Executor { get; set; }
        public Genre Genre { get; set; }
        public int id_Genre { get; set; }
        public string Path { get; set; }

        public Users User { get; set; }
        public int Id_User { get; set; }
    }
}
