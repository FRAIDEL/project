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
    public class ArticuloController : Controller
    {
        private hotelEntities5 db = new hotelEntities5();
        public static int IDhabitacion { get; set; }

        //
        // GET: /Articulo/

        public ViewResult Index()
        {
            var articulo = db.Articulo.Include(a => a.habitacion);
            return View(articulo.ToList());
        }

        //
        // GET: /Articulo/Details/5

        public ViewResult Details(int id)
        {
            Articulo articulo = db.Articulo.SingleOrDefault(a => a.ArticuloID == id);
            return View(articulo);
        }

        public ActionResult showMyArticulos(int id) {
            var articulo = db.Articulo.Where(a => a.HabitacionID == id);
            ArticuloController.IDhabitacion = id;
            return PartialView("Index", articulo);   
        }

        //
        // GET: /Articulo/Create

        public ActionResult Create()
        {
            //ViewBag.HabitacionID = new SelectList(db.habitaciones, "HabitacionID", "Num_habitacion");
            //var articulo = db.Articulo.Where(a => a.HabitacionID == id);
            //ArticuloController.IDhabitacion = id;
            //return PartialView("Index", articulo);
            return PartialView();
        } 

        //
        // POST: /Articulo/Create

        [HttpPost]
        public ActionResult Create(Articulo articulo)
        {
            if (ModelState.IsValid)
            {
                articulo.HabitacionID = ArticuloController.IDhabitacion;
                db.Articulo.AddObject(articulo);
                db.SaveChanges();
                var arts = db.Articulo.Where(a => a.HabitacionID == ArticuloController.IDhabitacion).ToList();
                return View("Index", arts);   
                //return RedirectToAction("Index");  
                //return PartialView("Create");
            }
            ViewBag.HabitacionID = new SelectList(db.Habitacion, "HabitacionID", "Num_habitacion", articulo.HabitacionID);
            return View(articulo);
        }
        
        //
        // GET: /Articulo/Edit/5
 
        public ActionResult Edit(int id)
        {
            Articulo articulo = db.Articulo.SingleOrDefault(a => a.ArticuloID == id);
            //var art = db.Articulo.SingleOrDefault(a => a.ArticuloID ==);
            ArticuloController.IDhabitacion = articulo.HabitacionID;
            ViewBag.HabitacionID = new SelectList(db.Habitacion, "HabitacionID", "Num_habitacion", articulo.HabitacionID);
            return PartialView(articulo);
        }

        //
        // POST: /Articulo/Edit/5

        [HttpPost]
        public ActionResult Edit(Articulo articulo)
        {
            try {
                articulo.HabitacionID = ArticuloController.IDhabitacion;
                if (ModelState.IsValid)
                {
                    //db.Entry(articulo).State = EntityState.Modified;
                    db.Articulo.Attach(articulo);
                    db.ObjectStateManager.ChangeObjectState(articulo, EntityState.Modified);

                    db.SaveChanges();
                    var art = db.Articulo.Where(a => a.HabitacionID == ArticuloController.IDhabitacion).ToList();
                    return View("Index", art);
                }
            }catch(Exception er){
                ViewBag.error = er.ToString();
            }
            
            ViewBag.HabitacionID = new SelectList(db.Habitacion, "HabitacionID", "Num_habitacion", articulo.HabitacionID);
            return PartialView(articulo);
        }

        //
        // GET: /Articulo/Delete/5
 
        public ActionResult Delete(int id)
        {
            Articulo articulo = db.Articulo.SingleOrDefault(a => a.ArticuloID == id);
            return View(articulo);
        }

        //
        // POST: /Articulo/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Articulo articulo = db.Articulo.SingleOrDefault(a => a.ArticuloID == id);
            db.Articulo.DeleteObject(articulo);
            db.SaveChanges();
            var art = db.Articulo.ToList();
            return PartialView("Index", art);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}