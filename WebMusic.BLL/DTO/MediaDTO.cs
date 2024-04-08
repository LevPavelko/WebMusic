using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMusic.DAL.Entities;

namespace WebMusic.BLL.DTO
{
    public class MediaDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        
        public int id_Executor { get; set; }
        public  string  Executor { get; set; }
        
        public int id_Genre { get; set; }
        public  string Genre { get; set; }
        public string? Path { get; set; }

        public int Id_User { get; set; }
        public string User { get; set; }
    }
}
