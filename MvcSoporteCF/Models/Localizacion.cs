using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcSoporteCF.Models
{
    public class Localizacion
    {
        public int Id { get; set; }
        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "La descripción de la localización es un campo requerido.")]
        public string Descripcion { get; set; }
        public virtual ICollection<Equipo> Equipos { get; set; }
    }
}