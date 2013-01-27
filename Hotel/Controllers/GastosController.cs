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
    public class GastosController : Controller
    {
        private hotelEntities5 db = new hotelEntities5();

        //
        // GET: /Gastos/

        public ViewResult Index()
        {
            return View(db.Gastos.ToList());
        }

        //
        // GET: /Gastos/Details/5

        public ViewResult Details(int id)
        {
            Gastos gasto = db.Gastos.SingleOrDefault(g => g.GastosID == id);
            return View(gasto);
        }

        //
        // GET: /Gastos/Create

        public ActionResult Create()
        {
            return PartialView();
        } 

        //
        // POST: /Gastos/Create

        [HttpPost]
        public ActionResult Create(Gastos gasto)
        {
            if (ModelState.IsValid)
            {
                db.Gastos.AddObject(gasto);
                db.SaveChanges();
                return View("index", db.Gastos.ToList()); 
            }
            return PartialView(gasto);
        }
        
        //
        // GET: /Gastos/Edit/5
 
        public ActionResult Edit(int id)
        {
            Gastos gasto = db.Gastos.SingleOrDefault(g => g.GastosID == id);
            return PartialView(gasto);
        }

        //
        // POST: /Gastos/Edit/5

        [HttpPost]
        public ActionResult Edit(Gastos gasto)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(gasto).State = EntityState.Modified;
                db.Gastos.Attach(gasto);
                db.ObjectStateManager.ChangeObjectState(gasto, EntityState.Modified);

                db.SaveChanges();
                return View("index", db.Gastos.ToList());
            }
            return PartialView(gasto);
        }

        //
        // GET: /Gastos/Delete/5
 
        public ActionResult Delete(int id)
        {
            Gastos gasto = db.Gastos.SingleOrDefault(g => g.GastosID == id);
            return View(gasto);
        }

        //
        // POST: /Gastos/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Gastos gasto = db.Gastos.SingleOrDefault(g => g.GastosID == id);
            db.Gastos.DeleteObject(gasto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}