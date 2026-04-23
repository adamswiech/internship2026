using System;
using System.Collections.Generic;
using System.Text;

namespace UsersApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public bool IsAdmin { get; set; } 
        public Nazwa Nazwa { get; set; }
        public Adres Adres { get; set; }
    }


}
