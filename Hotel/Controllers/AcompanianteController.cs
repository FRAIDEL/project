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
    public class AcompanianteController : Controller
    {
        private hotelEntities5 db = new hotelEntities5();

        //
        // GET: /Acompaniantes/

        public ActionResult Index(int id)
        {
            DateTime dt = DateTime.UtcNow.Date;
            //var Acompaniantess = db.Acompaniantess.Include(a => a.cliente);
            //return View(Acompaniantess.ToList());
            // consulto todos los Acompaniantess de este cliente y en este dia
            //List<Acompaniantes> acomp = db.Acompaniantes.Where(a => a.ClienteID == ClienteController.ID_Cliente
            List<Acompaniantes> acomp = db.Acompaniantes.Where(a => a.ClienteID == id
                && a.Fecha_actual == dt).ToList();
            ViewBag.id_cliente = ClienteController.ID_Cliente;
            return PartialView("_show_Acompaniantess", acomp);
        }

        //
        // GET: /Acompaniantes/Details/5

        public ViewResult Details(int id)
        {
            Acompaniantes Acompaniantes = db.Acompaniantes.SingleOrDefault(a => a.AcompanianteID == id);
            return View(Acompaniantes);
        }

        //
        // GET: /Acompaniantes/Create

        public ActionResult Create()
        {
            //ViewBag.ClienteID = new SelectList(db.Cliente, "ClienteID", "Identificacion");
            return PartialView("Create");
        } 

        //
        // POST: /Acompaniantes/Create

        [HttpPost]
        public ActionResult Create(Acompaniantes Acompaniantes)
        {
            try {
                DateTime currentDate = DateTime.UtcNow.Date;
                Acompaniantes.ClienteID = ClienteController.ID_Cliente;
                Acompaniantes.Fecha_actual = DateTime.UtcNow;
                if (ModelState.IsValid)
                {
                    db.Acompaniantes.AddObject(Acompaniantes);
                    db.SaveChanges();
                    // consulto todos los Acompaniantess de este cliente y en este dia
                    //List<Acompaniantes> acomp = db.Acompaniantes.Where(a => a.ClienteID == ClienteController.ID_Cliente 
                    IEnumerable<Acompaniantes> acomp = db.Acompaniantes.Where(a => a.ClienteID == ClienteController.ID_Cliente
                        && a.Fecha_actual == currentDate);
                    ViewBag.id_cliente = ClienteController.ID_Cliente;
                    return PartialView("_show_Acompaniantes", acomp);
                }
            }catch(Exception er){
                ViewBag.error = er.ToString();
            }
            //ViewBag.ClienteID = new SelectList(db.Cliente, "ClienteID", "Identificacion", Acompaniantes.ClienteID);
            return PartialView(Acompaniantes);
        }
        
        //
        // GET: /Acompaniantes/Edit/5
 
        public ActionResult Edit(int id)
        {
            Acompaniantes Acompaniantes = db.Acompaniantes.SingleOrDefault(a => a.AcompanianteID == id);
            //ViewBag.ClienteID = new SelectList(db.Cliente, "ClienteID", "Identificacion", Acompaniantes.ClienteID);
            //return View(Acompaniantes);
            return PartialView(Acompaniantes);
        }

        //
        // POST: /Acompaniantes/Edit/5

        [HttpPost]
        public ActionResult Edit(Acompaniantes Acompaniantes)
        {
            try {
                Acompaniantes.ClienteID = ClienteController.ID_Cliente;
                Acompaniantes.Fecha_actual = DateTime.Today;
                if (ModelState.IsValid)
                {
                    //db.Entry(Acompaniantes).State = EntityState.Modified;
                    db.Acompaniantes.Attach(Acompaniantes);
                    db.ObjectStateManager.ChangeObjectState(Acompaniantes, EntityState.Modified);

                    db.SaveChanges();
                    //return RedirectToAction("Index");
                    // consulto todos los Acompaniantess de este cliente y en este dia
                    List<Acompaniantes> acomp = db.Acompaniantes.Where(a => a.ClienteID == ClienteController.ID_Cliente
                        && a.Fecha_actual == DateTime.Today).ToList();
                    return PartialView("_show_Acompaniantess", acomp);
                }
            }catch(Exception er){
                ViewBag.error = er.ToString();
            }
            //ViewBag.ClienteID = new SelectList(db.Cliente, "ClienteID", "Identificacion", Acompaniantes.ClienteID);
            return View(Acompaniantes);
        }

        //
        // GET: /Acompaniantes/Delete/5
 
        public ActionResult Delete(int id)
        {
            Acompaniantes Acompaniantes = db.Acompaniantes.SingleOrDefault(a => a.AcompanianteID == id);
            return View(Acompaniantes);
        }

        //
        // POST: /Acompaniantes/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Acompaniantes Acompaniantes = db.Acompaniantes.SingleOrDefault(a => a.AcompanianteID == id);
            db.Acompaniantes.DeleteObject(Acompaniantes);
            db.SaveChanges();
            // consulto todos los Acompaniantess de este cliente y en este dia
            List<Acompaniantes> acomp = db.Acompaniantes.Where(a => a.ClienteID == ClienteController.ID_Cliente
                && a.Fecha_actual == DateTime.Today).ToList();
            return PartialView("_show_Acompaniantess", acomp);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        //[HttpPost]
        //public ActionResult Add_Acompaniantess(Acompaniantes Acompaniantes) {
        //    Acompaniantes.ClienteID = ClienteController.ID_Cliente;
        //    Acompaniantes.Fecha_actual = DateTime.Now;
        //    db.Acompaniantess.Add(Acompaniantes);
        //    db.SaveChanges();
        //    //creo el model para el partialView
        //    // consulto todos los Acompaniantess de este cliente y en este dia
        //    List<Acompaniantes> acomp = db.Acompaniantess.Where(a => a.ClienteID == ClienteController.ID_Cliente
        //        && a.Fecha_actual == DateTime.Today).ToList();
        //    return PartialView("_show_Acompaniantess", acomp);
        //}
    }
}