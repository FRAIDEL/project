using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Models
{
    public class Verificar_cliente
    {
        [Key]
        public string cedula_titular { get; set; }
    }
}