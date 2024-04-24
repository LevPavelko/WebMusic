using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMusic.DAL.Entities
{
    public class FavSongs
    {
        public int Id { get; set; }
        public int id_Song { get; set; }
        public virtual Media media { get; set; }
        public int id_User { get; set; }
        public virtual Users user { get; set; }
    }
}
