using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Models
{
    public class query_hab_disponibles
    {
        [Key]
        [Required(ErrorMessage="El campo de 'Fecha Incial' es OBLIGATORIO..!")]
        [Display(Name="fecha inicial")]
        public DateTime date_init { get; set; }
        [Required(ErrorMessage = "El campo de 'Fecha Final' es OBLIGATORIO..!")]
        [Display(Name = "fecha final")]
        public DateTime date_end { get; set; }
    }
}