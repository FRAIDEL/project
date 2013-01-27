using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Hotel.Models;

//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using Hotel.Models;

namespace Hotel.Setting
{
    public class _RegistroCliente
    {
        public int ClienteID { get; set; }
        public int? HabitacionID { get; set; }

        public DateTime? Fecha_ingreso { get; set; } // fecha en q llega
        public DateTime? Fecha_salida { get; set; } // fecha en q sale
        public int? Cantidad_dias { get; set; }
        public int? Total { get; set; }
        public int? Abonar { get; set; }
        public int? Restante { get; set; }
        public DateTime? Fecha_entrega { get; set; } // fecha en q se entrega la habitacion
        public TimeSpan? hora_entrega { get; set; } // hora en q se entrega la habitacion
        public DateTime? Fecha_devolucion { get; set; } // fecha en q se devolucion la habitacion
        public TimeSpan? hora_devolucion { get; set; } // hora en q se devolucion la habitacion
        public string Observaciones { get; set; }
        public TimeSpan hora_ingreso { get; set; }

        public virtual Cliente cliente { get; set; }
        public virtual Habitacion habitacion { get; set; }
        public virtual IEnumerable<Acompaniantes> acpetes { get; set; }
        //public virtual Acompaniantes acpetes { get; set; }
    }
}