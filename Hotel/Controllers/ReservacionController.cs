using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hotel.Controllers;
using ReportManagement;
using Hotel.Models;

namespace Hotel.Controllers
{
    public class ReservacionController : /*Controller*/PdfViewController
    {
        private hotelEntities5 db = new hotelEntities5();
        //public static string NameHabitacion { get; set;}

        //
        // GET: /Reservacion/

        public ViewResult Index()
        {
            var reservaciones = db.Reserva.ToList();//.Include(r => r.cliente).Include(r => r.detalles_reservacion);
            //return View(reservaciones.Where(r => r.hora_devolucion != null).ToList());
            return View(reservaciones);
        }

        //
        // GET: /Reservacion/Details/5

        public ActionResult Details(int id)
        {
            Reserva reserva = db.Reserva.SingleOrDefault(r => r.ReservacionID == id);
            return PartialView(reserva);
        }

        //
        // GET: /Reservacion/Create

        public ActionResult Create(int id)
        {
            var habit = db.Habitacion.SingleOrDefault(h => h.HabitacionID == id);
            TimeSpan current_time = DateTime.UtcNow.TimeOfDay;
            ViewBag.hora = current_time.Hours + ":" + current_time.Minutes + ":" + current_time.Seconds;
            ViewBag.HabitacionID_ = habit.HabitacionID;
            ViewBag.num_hab = habit.Num_habitacion;
            return PartialView("Create");
        } 

        //
        // POST: /Reservacion/Create

        [HttpPost]
        public ActionResult Create(Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                if (valid_Date(reserva.Fecha_ingreso, reserva.Fecha_egreso))
                {
                    int cont = new int();
                    cont = 0;
                    // busco las reservas de esa habitacion
                    var res = db.Reserva.Where(r => r.HabitacionID_ == reserva.HabitacionID_);
                    // valido q la habitacion este disponible
                    foreach (Reserva r in res)
                    {
                        // consulto si la habitacion no esta ocupada en esa fecha
                        if(!(reserva.Fecha_ingreso < r.Fecha_ingreso
                            && reserva.Fecha_egreso <= r.Fecha_ingreso
                            || reserva.Fecha_ingreso >= r.Fecha_egreso) )
                        {
                            // esta ocupada
                            cont++;
                        }
                    }
                    if (cont > 0)
                    {
                        var hbt = db.Habitacion.SingleOrDefault(h => h.HabitacionID == reserva.HabitacionID_);
                        ViewBag.no_disponible = "LA HABITACION " + hbt.Num_habitacion + " NO ESTA DISPONIBLE EN EL RANGO DE FECHAS SOLICITADO..! ";
                    }
                    else {
                        // entonces creo la reserva
                        reserva.IP_creacion = Request.ServerVariables["REMOTE_ADDR"];
                        TimeSpan? count_dias = (reserva.Fecha_egreso - reserva.Fecha_ingreso);

                        reserva.Cantidad_dias = count_dias.Value.Days;
                        //reserva.Fecha_entrega = DateTime.UtcNow.Date;
                        reserva.Fecha_devolucion = DateTime.UtcNow.Date;
                        if (reserva.Estado == "true")
                        {
                            //reserva.Hora_entrega = DateTime.UtcNow.TimeOfDay;//.UtcNow.ToShortTimeString();
                            // OJO si hora_devolucion es null (no se ha devuelto la Habitacion)
                            reserva.Hora_devolucion = null;
                        }
                        else
                        {
                            // OJO si hora_entrega y hora_devolucion es null (ni siquiera se ha entregado la Habitacion)
                            //reserva.Hora_entrega = null;
                            reserva.Hora_devolucion = null;
                        }
                        try
                        {
                            // guardo la reservacion
                            reserva.ClienteID_ = ClienteController.ID_Cliente;
                            //reserva.ClienteID = 1;
                            reserva.Fecha_creacion = DateTime.UtcNow.Date;
                            reserva.Fecha_ultima_modificacion = null;// DateTime.UtcNow.Date;
                            db.Reserva.AddObject(reserva);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        catch (Exception er)
                        {
                            ViewBag.error = er.ToString();
                        }
                    }
                }
                else { ViewBag.fecha_incorrecta = "La fecha de Ingreso es MENOR o Igual a la fecha de SALIDA, por favor rectifique..!"; }
                    
                // valido las fechas
                
            }
            return View(reserva);
        }
        // valida las fechas
        public bool valid_Date(DateTime? date_ingreso, DateTime? date_salida) {
            if (date_salida <= date_ingreso)
            {
                return false;
            }
            return true;
        }
        
        //
        // GET: /Reservacion/Edit/5
 
        public ActionResult Edit(int id)
        {
            Reserva reserva = db.Reserva.SingleOrDefault(r => r.ReservacionID == id);
            return PartialView(reserva);
        }

        //
        // POST: /Reservacion/Edit/5

        [HttpPost]
        public ActionResult Edit(Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                try {
                    TimeSpan? count_dias = reserva.Fecha_egreso - reserva.Fecha_ingreso;
                    reserva.IP_modificacion = Request.ServerVariables["REMOTE_ADDR"];
                    reserva.Fecha_ultima_modificacion = DateTime.UtcNow.Date;
                    //reserva.User_modificacion = HabitacionController.currentUser;
                    //db.Entry(reserva).State = EntityState.Modified;
                    db.Reserva.Attach(reserva);
                    db.ObjectStateManager.ChangeObjectState(reserva, EntityState.Modified);


                    //reserva.Fecha_entrega = DateTime.UtcNow.Date;
                    reserva.Fecha_devolucion = null;// DateTime.UtcNow.Date;
                    if (reserva.Estado == "true")
                    {
                        //reserva.Fecha_entrega = DateTime.UtcNow;
                        //reserva.Hora_entrega = DateTime.UtcNow.TimeOfDay;//.ToShortTimeString();
                        //reserva.Fecha_devolucion = DateTime.UtcNow;
                        // OJO si hora_devolucion es null (no se ha devuelto la Habitacion)
                        reserva.Hora_devolucion = null;
                    }
                    else
                    {                        
                        // OJO si hora_entrega y hora_devolucion es null (ni siquiera se ha entregado la Habitacion)
                        //reserva.Hora_entrega = null;
                        reserva.Hora_devolucion = null;
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");   
                }catch(Exception er){
                    ViewBag.error = er.ToString(); 
                }
            }
            return View(reserva);
            //return RedirectToAction("Index"); 
        }

        //
        // GET: /Reservacion/Delete/5
 
        public ActionResult Delete(int id)
        {
            Reserva reserva = db.Reserva.SingleOrDefault(r => r.ReservacionID ==  id);
            return View(reserva);
        }

        //
        // POST: /Reservacion/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Reserva reserva = db.Reserva.SingleOrDefault(r => r.ReservacionID == id);
            db.Reserva.DeleteObject(reserva);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult RegistroHuesped_PDF(int id)
        {
            TimeSpan current_time = DateTime.UtcNow.TimeOfDay;
            Reserva res = db.Reserva.SingleOrDefault(r => r.ReservacionID == id);
            //List<Acompaniante> acomp = res.cliente.Acompaniante;
            //foreach(var i in res.cliente.Acompaniante){
            //    acomp.Add(i);
            //}
            //Habitacion hbt = db.Habitacion.SingleOrDefault(h => h.HabitacionID == res.HabitacionID_);
            _RegistroCliente rq = new _RegistroCliente
            {
                ClienteID = res.ClienteID_,
                HabitacionID = res.HabitacionID_,
                Fecha_ingreso = res.Fecha_ingreso,
                Fecha_salida = res.Fecha_egreso,
                Cantidad_dias = res.Cantidad_dias,
                Total = res.Total,
                Abonar = res.Abono,
                Restante = res.Restante,
                //Fecha_entrega = res.Fecha_entrega,
                //hora_entrega = res.Hora_entrega,
                Fecha_devolucion = res.Fecha_devolucion,
                hora_devolucion = res.Hora_devolucion,
                cliente = res.cliente,
                //habitacion = hbt,
                habitacion = res.habitacion,
                Observaciones = res.Observacion,
                hora_ingreso = current_time,
                acpetes = db.Acompaniantes.Where(a => a.ClienteID == res.ClienteID_).ToList(),
            };
            return this.ViewPdf("",
                "RegistroHuesped_PDF", rq);
        }

    }
    
}