using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Hotel.Models
{
    public class query_cliente
    {
        [Key]
        public string identificacion { get; set; }
    }
}