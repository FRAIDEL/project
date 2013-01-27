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
    public class CheckOutController : Controller
    {
        private AccessData db = new AccessData();

        //
        // GET: /CheckOut/

        public ViewResult Index()
        {
            var chectouts = db.chectOuts.Include(c => c.habitacion);
            return View(chectouts.ToList());
        }

        //
        // GET: /CheckOut/Details/5

        public ViewResult Details(int id)
        {
            CheckOut checkout = db.chectOuts.Find(id);
            return View(checkout);
        }

        //
        // GET: /CheckOut/Create

        public ActionResult Create()
        {
            ViewBag.HabitacionID = new SelectList(db.habitaciones, "HabitacionID", "Nombre_habitacion");
            return View();
        } 

        //
        // POST: /CheckOut/Create

        [HttpPost]
        public ActionResult Create(CheckOut checkout)
        {
            if (ModelState.IsValid)
            {
                db.chectOuts.Add(checkout);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.HabitacionID = new SelectList(db.habitaciones, "HabitacionID", "Nombre_habitacion", checkout.HabitacionID);
            return View(checkout);
        }
        
        //
        // GET: /CheckOut/Edit/5
 
        public ActionResult Edit(int id)
        {
            CheckOut checkout = db.chectOuts.Find(id);
            ViewBag.HabitacionID = new SelectList(db.habitaciones, "HabitacionID", "Nombre_habitacion", checkout.HabitacionID);
            return View(checkout);
        }

        //
        // POST: /CheckOut/Edit/5

        [HttpPost]
        public ActionResult Edit(CheckOut checkout)
        {
            if (ModelState.IsValid)
            {
                db.Entry(checkout).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HabitacionID = new SelectList(db.habitaciones, "HabitacionID", "Nombre_habitacion", checkout.HabitacionID);
            return View(checkout);
        }

        //
        // GET: /CheckOut/Delete/5
 
        public ActionResult Delete(int id)
        {
            CheckOut checkout = db.chectOuts.Find(id);
            return View(checkout);
        }

        //
        // POST: /CheckOut/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            CheckOut checkout = db.chectOuts.Find(id);
            db.chectOuts.Remove(checkout);
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