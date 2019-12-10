using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSoporteCF.Models
{
    public class UsuarioConRol
    {
        public string Email { get; set; }
        public string NombreUsuario { get; set; }
        public string RolDelUsuario { get; set; }
    }
}