using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcSoporteCF.Models
{
    public class Empleado
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre del empleado es un campo requerido.")]
        public string Nombre { get; set; }
        [Display(Name = "Correo electrónico")]
        [EmailAddress]
        public string Email { get; set; }
        public virtual ICollection<Aviso> Avisos { get; set; }

    }
}