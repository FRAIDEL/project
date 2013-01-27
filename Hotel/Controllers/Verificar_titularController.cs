using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hotel.Models;

namespace Hotel.Controllers
{ 
    public class Verificar_titularController : Controller
    {
        private hotelEntities5 db = new hotelEntities5();

        [HttpPost]
        public ActionResult client() {
            return PartialView("_Verif_cliente_titular");
        }

        [HttpPost]
        public ActionResult verificar(Verificar_cliente verificar_cliente)
        {
            Cliente titular = db.Cliente.SingleOrDefault(c => c.Identificacion == verificar_cliente.cedula_titular);
            if (titular != null)
            {
                ClienteController.ID_Cliente = titular.ClienteID;
                // muestro la vista q me muestra datos de este
                return PartialView("_Cliente_registrado", titular);

            }
            return RedirectToAction("Create", "Cliente");
        }        
    }
}