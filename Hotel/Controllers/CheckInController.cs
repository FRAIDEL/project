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
    public class CheckInController : Controller
    {
        private AccessData db = new AccessData();

        //
        // GET: /CheckIn/

        public ViewResult Index()
        {
            var chectins = db.chectIns.Include(c => c.habitacion);
            return View(chectins.ToList());
        }

        //
        // GET: /CheckIn/Details/5

        public ViewResult Details(int id)
        {
            CheckIn checkin = db.chectIns.Find(id);
            return View(checkin);
        }

        //
        // GET: /CheckIn/Create

        public ActionResult Create()
        {
            ViewBag.HabitacionID = new SelectList(db.habitaciones, "HabitacionID", "Nombre_habitacion");
            //return View();
            return PartialView();
        } 

        //
        // POST: /CheckIn/Create

        [HttpPost]
        public ActionResult Create(CheckIn checkin)
        {
            if (ModelState.IsValid)
            {
                db.chectIns.Add(checkin);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.HabitacionID = new SelectList(db.habitaciones, "HabitacionID", "Nombre_habitacion", checkin.HabitacionID);
            return View(checkin);
        }
        
        //
        // GET: /CheckIn/Edit/5
 
        public ActionResult Edit(int id)
        {
            CheckIn checkin = db.chectIns.Find(id);
            ViewBag.HabitacionID = new SelectList(db.habitaciones, "HabitacionID", "Nombre_habitacion", checkin.HabitacionID);
            return View(checkin);
        }

        //
        // POST: /CheckIn/Edit/5

        [HttpPost]
        public ActionResult Edit(CheckIn checkin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(checkin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HabitacionID = new SelectList(db.habitaciones, "HabitacionID", "Nombre_habitacion", checkin.HabitacionID);
            return View(checkin);
        }

        //
        // GET: /CheckIn/Delete/5
 
        public ActionResult Delete(int id)
        {
            CheckIn checkin = db.chectIns.Find(id);
            return View(checkin);
        }

        //
        // POST: /CheckIn/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            CheckIn checkin = db.chectIns.Find(id);
            db.chectIns.Remove(checkin);
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