using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcSoporteCF.Models
{
    public class Equipo
    {
        public int Id { get; set; }
        [Display(Name = "Código de equipo")]
        [Required(ErrorMessage = "El código del equipo es un campo requerido.")]
        public string CodigoEquipo { get; set; }
        [Required(ErrorMessage = "La marca es un campo requerido.")]
        public string Marca { get; set; }
        [Required(ErrorMessage = "El modelo es un campo requerido.")]
        public string Modelo { get; set; }
        [Display(Name = "Número de serie")]
        [Required(ErrorMessage = "El número de serie es un campo requerido.")]
        public string NumeroSerie { get; set; }
        [Display(Name = "Características técnicas")]
        public string Caracteristicas { get; set; }
        [Display(Name = "Fecha de compra")]
        [Required(ErrorMessage = "La fecha de compra es un campo requerido.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime FechaCompra { get; set; }
        [Display(Name = "Fecha de puesta en servicio")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FechaAlta { get; set; }
        [Display(Name = "Fecha de retirada del servicio")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FechaBaja { get; set; }
        [Display(Name = "Localización")]
        public int LocalizacionId { get; set; }
        public virtual ICollection<Aviso> Avisos { get; set; }
        public virtual Localizacion Localizacion { get; set; }
    }
}