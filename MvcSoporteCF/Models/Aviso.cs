using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcSoporteCF.Models
{
    public class Aviso
    {
        public int Id { get; set; }
        [Display(Name = "Descripción del problema")]
        [Required(ErrorMessage = "La descripción del aviso es un campo requerido.")]
        public string Descripcion { get; set; }
        [Display(Name = "Fecha de aviso")]
        [Required(ErrorMessage = "La fecha en que se realiza el aviso es un campo requerido.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime FechaAviso { get; set; }
        [Display(Name = "Fecha de cierre")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FechaCierre { get; set; }
        [Display(Name = "Observaciones")]
        public string Observaciones { get; set; }
        [Display(Name = "Empleado")]
        public int EmpleadoId { get; set; }
        [Display(Name = "Ipo de avería")]
        public int TipoAveriaId { get; set; }
        [Display(Name = "Equipo")]
        public int EquipoId { get; set; }
        public virtual Empleado Empleado { get; set; }
        public virtual TipoAveria TipoAveria { get; set; }
        public virtual Equipo Equipo { get; set; }
    }
}