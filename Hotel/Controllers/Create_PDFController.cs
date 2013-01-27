using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hotel.Models;
using ReportManagement;

namespace Hotel.Controllers
{ 
    //public class Create_PDFController : Controller
    //{
    //    private hotelEntities dbs = new hotelEntities();

    //public class Create_PDFController : PdfViewController
    //{            
    //        private hotelEntities db = new hotelEntities();

    //        public ActionResult RegistroHuesped_PDF(int id) {
    //            var reser = db.reservaciones.SingleOrDefault(r => r.ReservaID == id);
    //            ViewBag.numero = reser.habitacion.Num_habitacion;
    //            var cliente_current = reser.cliente;
                
    //            return this.ViewPdf("REGISTRO OBLIGATORIO DE VISITANTES",
    //                "RegistroHuesped_PDF", cliente_current);
    //        }

           
    //    }

    // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        //
        // GET: /GeneraRegistroCliente_PDF/

        //public ViewResult Index()
        //{
        //    return View(db.Cliente.ToList());
        //}

        ////
        //// GET: /GeneraRegistroCliente_PDF/Details/5

        //public ViewResult Details(int id)
        //{
        //    Cliente cliente = db.Cliente.Find(id);
        //    return View(cliente);
        //}

        ////
        //// GET: /GeneraRegistroCliente_PDF/Create

        //public ActionResult Create()
        //{
        //    return View();
        //} 

        ////
        //// POST: /GeneraRegistroCliente_PDF/Create

        //[HttpPost]
        //public ActionResult Create(Cliente cliente)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Cliente.Add(cliente);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");  
        //    }

        //    return View(cliente);
        //}
        
        ////
        //// GET: /GeneraRegistroCliente_PDF/Edit/5
 
        //public ActionResult Edit(int id)
        //{
        //    Cliente cliente = db.Cliente.Find(id);
        //    return View(cliente);
        //}

        ////
        //// POST: /GeneraRegistroCliente_PDF/Edit/5

        //[HttpPost]
        //public ActionResult Edit(Cliente cliente)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(cliente).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(cliente);
        //}

        ////
        //// GET: /GeneraRegistroCliente_PDF/Delete/5
 
        //public ActionResult Delete(int id)
        //{
        //    Cliente cliente = db.Cliente.Find(id);
        //    return View(cliente);
        //}

        ////
        //// POST: /GeneraRegistroCliente_PDF/Delete/5

        //[HttpPost, ActionName("Delete")]
        //public ActionResult DeleteConfirmed(int id)
        //{            
        //    Cliente cliente = db.Cliente.Find(id);
        //    db.Cliente.Remove(cliente);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    db.Dispose();
        //    base.Dispose(disposing);
        //}
    }
//}