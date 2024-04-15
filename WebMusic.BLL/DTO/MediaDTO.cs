using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMusic.DAL.Entities;

namespace WebMusic.BLL.DTO
{
    public class MediaDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string Title { get; set; }
        
        public int id_Executor { get; set; }
        public  string  Executor { get; set; }
        
        public int id_Genre { get; set; }
        public  string Genre { get; set; }

        public string? Path { get; set; }

        public int Id_User { get; set; }
        //public string User { get; set; }
    }
}
