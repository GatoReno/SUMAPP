using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SUMATEAPPT2.Interfaces
{
    public class UserFS
    {
        
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Sucursal { get; set; }
        public string Email { get; set; }
        public string Mensaje { get; set; }
    }
}
