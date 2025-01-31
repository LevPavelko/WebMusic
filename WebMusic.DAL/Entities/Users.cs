﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMusic.DAL.Entities
{
    public class Users
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public int? Status { get; set; }
        public virtual ICollection<Media> media { get; set; }

        public virtual ICollection<FavSongs> favSongs { get; set; }
    }
}
