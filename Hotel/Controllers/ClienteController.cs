using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hotel.Models;
using System.IO;

namespace Hotel.Controllers
{ 
    public class ClienteController : Controller
    {
        //private hotelEntities db = new hotelEntities();
        private hotelEntities5 db = new hotelEntities5();
        public static int ID_Cliente { get; set;}
        //
        // GET: /Cliente/

        public ViewResult Index()
        {
            return View(db.Cliente.ToList());
        }

        //
        // GET: /Cliente/Details/5

        public ActionResult Details(int id)
        {
            Cliente cliente = db.Cliente.SingleOrDefault(c => c.ClienteID == id);
            // almaceno el id del cliente q actual, q lo necesito para trabajar con el acompaniante
            ClienteController.ID_Cliente = id;
            return PartialView(cliente);
        }

        // GET: /Cliente/Create
        public ActionResult Create()
        {
            return PartialView("Create");
        }

        [HttpGet]
        public ActionResult getDataCliente() {
            return PartialView();
        }

        [HttpPost]
        public ActionResult getDataCliente(query_cliente id) {
            Cliente clt = db.Cliente.SingleOrDefault(c => c.Identificacion == id.identificacion);
            return PartialView("_Cliente_registrado", clt);
        }

        //
        // POST: /Cliente/Create

        [HttpPost]
        public ActionResult Create(HttpPostedFileBase file, Cliente cliente/*, HttpPostedFileBase file*/)
        {
            if (ModelState.IsValid)
            {
                //file.SaveAs(Path.Combine(Url.Content("/Content/"), Path.GetFileName(file.FileName)));
                //if (file.ContentLength > 0)
                //{
                //    var fileName = Path.GetFileName(file.FileName);
                //    var path = Path.Combine(Server.MapPath("~/Content/"), file.FileName);
                //    file.SaveAs(path);
                //}
    
                // verifico q el nuevo cliente no este en la db
                var valid_cliente = db.Cliente.SingleOrDefault(c => c.Identificacion == cliente.Identificacion);
                if (valid_cliente == null)
                {
                    try {
                        db.Cliente.AddObject(cliente);
                        db.SaveChanges();
                        // consulto por el id del cliente
                        Cliente current_client = db.Cliente.SingleOrDefault(c =>
                            c.Identificacion == cliente.Identificacion);
                        ////obtengo el id del cliente 'lo necesito en reserva'
                        ClienteController.ID_Cliente = current_client.ClienteID;
                        return PartialView("_Cliente_registrado", cliente);
                    }catch(Exception e){
                        ViewBag.errorCreate = e.ToString();
                    }
                }
                else {
                    ViewBag.cliente_registrado = "El Cliente con numero de Identificacion " + cliente.Identificacion.ToString() +" ya se encuentra en la Base De Datos, ingrese nuevamente la Identificacion en el campo 'Cedula del Titular' y verifiquela; asi lograra cargar la informacion de este Cliente..! ";
                }
            }
            //return PartialView("_test", cliente);
            return PartialView(cliente);
        }
        
        //
        // GET: /Cliente/Edit/5
 
        public ActionResult Edit(int id)
        {
            Cliente cliente = db.Cliente.SingleOrDefault(c => c.ClienteID == id);
            ClienteController.ID_Cliente = cliente.ClienteID;
            return PartialView(cliente);
            //return PartialView(cliente);
        }

        //
        // POST: /Cliente/Edit/5

        [HttpPost]
        public ActionResult Edit(Cliente cliente)
        {
            cliente.ClienteID = ClienteController.ID_Cliente;
            if (ModelState.IsValid)
            {
                //db.Entry(cliente).State = EntityState.Modified;
                db.Cliente.Attach(cliente);
                db.ObjectStateManager.ChangeObjectState(cliente, EntityState.Modified);
                db.SaveChanges();
                return PartialView("_Cliente_registrado", cliente);
            }
            return View(cliente);
        }

        //
        // GET: /Cliente/Delete/5
 
        public ActionResult Delete(int id)
        {
            Cliente cliente = db.Cliente.SingleOrDefault(c => c.ClienteID == id);
            return View(cliente);
        }

        //
        // POST: /Cliente/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            // elimino los Acompaniantes de este cliente
            List<Acompaniantes> acomp = db.Acompaniantes.Where(a => a.ClienteID == id 
                && a.Fecha_actual == DateTime.Today).ToList();
            foreach(var i in acomp){
                db.Acompaniantes.DeleteObject(i);
                db.SaveChanges();
            }
            // consulto si estubo en algun momento en el hotel, de no ser asi
            // elimino este cliente
            List<Acompaniantes> client = db.Acompaniantes.Where(a => a.ClienteID == id).ToList();
            if(client == null){
                // quiere decir q no ha estado en el hotel con anterioridad
                // elimino al cliente
                Cliente cliente = db.Cliente.SingleOrDefault(c => c.ClienteID == id);
                db.Cliente.DeleteObject(cliente);
                db.SaveChanges();
            }
            return PartialView("_Cliente_registrado");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}