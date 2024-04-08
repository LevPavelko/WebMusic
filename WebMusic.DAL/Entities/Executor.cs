using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMusic.DAL.Entities
{
    public class Executor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Media> media { get; set; }

    }
}
