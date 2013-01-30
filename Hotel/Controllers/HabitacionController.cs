using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hotel.Models;
using System.IO;
using System.Drawing;
using System.Web.Security;
using System.Web.Routing;

namespace Hotel.Controllers
{
    // [Authorize(Users="fraidel3")]
    // [Authorize(Roles = "Administrador")]
    public class HabitacionController : Controller
    {
        //private hotelEntities db = new hotelEntities();
        private hotelEntities5 db = new hotelEntities5();
        private static Byte[] BytesImg { get; set; }

        //[HttpPost]
        //public ActionResult client() {
        //    return PartialView("_Verif_cliente_titular");
        //}

        //[HttpPost]
        //public ActionResult verificar(Verificar_cliente verificar_cliente) {
        //    var titular = db.Cliente.SingleOrDefault(c => c.Identificacion == verificar_cliente.cedula_titular);
        //    if(titular != null){
        //        try {
        //            //ClienteController.ID_Cliente = titular.ClienteID;
        //            // muestro la vista q me muestra datos de este
        //            return PartialView("_Cliente_registrado", titular);   
        //        }catch(Exception er){
        //            ViewBag.error = "error en verificar " + er.ToString();
        //        }
        //    }
        //    return RedirectToAction("Create", "Cliente");
        //}        

        public ActionResult Home() {
            DateTime fecha = DateTime.UtcNow;
            ViewBag.horaCompleta = DateTime.UtcNow.ToShortTimeString();
            ViewBag.fecha = DateTime.UtcNow.ToShortDateString();
            ViewBag.horas = DateTime.UtcNow.Hour - 5;
            ViewBag.minutos = DateTime.UtcNow.Minute;
            ViewBag.segundos = DateTime.UtcNow.Second;
            return View();
        }

        // GET: /Habitacion/
        public ViewResult Index()
        {
            return View(db.Habitacion.ToList());
        }

        // get
        [HttpGet]
        public ActionResult show_hab_disponibles()
        {
            return PartialView("_query_disponibles");
        }

        //// post
        [HttpPost]
        public ActionResult show_hab_disponibles(query_hab_disponibles Query_Disp)
        {
            List<Habitacion> habitacions = db.Habitacion.ToList();
            List<Habitacion> habit_to_list = db.Habitacion.ToList();
            List<Habitacion> habitFree = new List<Habitacion>();
            // consulto x c/u de las habitaciones
            foreach (Habitacion habit in habitacions)
            {
                foreach (Reserva r in habit.reserva)
                {
                    // consulto si la habitacion no esta ocupada en esa fecha
                    if (!(Query_Disp.date_init < r.Fecha_ingreso
                          && Query_Disp.date_end <= r.Fecha_ingreso
                          || Query_Disp.date_init >= r.Fecha_egreso))
                    {
                        habit_to_list.Remove(r.habitacion);
                        
                    }
                }
            }
            return View("Index", habit_to_list);
        }

        //public ActionResult listDisp() {
        //    return PartialView(db.habitaciones.ToList());
        //}

        //
        // GET: /Habitacion/Details/5

        public ViewResult Details(int id)
        {
            var habitacion = db.Habitacion.SingleOrDefault(h => h.HabitacionID == id);
            return View(habitacion);
        }

        // validar el numero de la habitacion
        public bool numHabitacion_Unique(string n)
        {
            var numsHabitacions = db.Habitacion.ToList();
            int cont = 0;
            foreach (var hab in numsHabitacions)
            {
                if (n == hab.Num_habitacion)
                {
                    cont++;
                }
            }
            if (cont > 0)
            {
                return false;
            }
            else { return true; }
        }

        //
        // GET: /Habitacion/Create

        public ActionResult Create()
        {
            return PartialView();
        }

        //
        // POST: /Habitacion/Create

        //[HttpPost]
        //[AcceptVerbs(HttpVerbs.Post), ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Habitacion habit)
        {
            habit.Estado = "libre";
            if (ModelState.IsValid)
            {
                if (numHabitacion_Unique(habit.Num_habitacion))
                {
                    try
                    {
                        db.Habitacion.AddObject(habit);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (Exception er)
                    {
                        ViewBag.error = er.ToString();
                    }
                }
                else
                {
                    ViewBag.error_num_habitacion = "Ya existe una habitacion con el numero '" + habit.Num_habitacion + "' Por Favor rectifique he intente nuevamente..! ";
                    return View(habit);
                }
            }
            return View(habit);
        }

        //
        // GET: /Habitacion/Edit/5

        public ActionResult Edit(int id)
        {
            Habitacion habitacion = db.Habitacion.SingleOrDefault(h => h.HabitacionID == id);
            var habit = new HabitacionController();
            ////habit.BytesImg = habitacion.Image.ToArray();
            //HabitacionController.BytesImg = habitacion.Image.ToArray();
            ////ViewBag.b = this.BytesImg[17];
            return View(habitacion);
        }

        //
        // POST: /Habitacion/Edit/5

        [HttpPost]
        public ActionResult Edit(Habitacion habitacion)
        {
            habitacion.Estado = "libre";
            if (ModelState.IsValid)
            {
                db.Habitacion.Attach(habitacion);
                db.ObjectStateManager.ChangeObjectState(habitacion, EntityState.Modified);
                
                ////db.Entry(habitacion).State = EntityState.Modified;
                //habitacion.Image = HabitacionController.BytesImg;
                habitacion.Descripcion = habitacion.Descripcion;
                ////habitacion.Estado = habitacion.Estado;
                //habitacion.Nombre_habitacion = habitacion.Nombre_habitacion;
                habitacion.Num_habitacion = habitacion.Num_habitacion;
                habitacion.Num_piso = habitacion.Num_piso;
                db.SaveChanges();
                HabitacionController.BytesImg = null;
                return RedirectToAction("Index");
            }
            return View(habitacion);
        }

        //
        // GET: /Habitacion/Delete/5

        public ActionResult Delete(int id)
        {
            Habitacion habitacion = db.Habitacion.SingleOrDefault(h => h.HabitacionID == id);
            db.Habitacion.DeleteObject(habitacion);
            db.SaveChanges();
            // consulto las habitaciones para mostrarlas en la vista parcial
            var habts = db.Habitacion.ToList();
            return PartialView("Index", habts);
        }

        //
        // POST: /Habitacion/Delete/5

        //[HttpPost, ActionName("Delete")]
        //public ActionResult DeleteConfirmed(int id)
        //{            
        //    Habitacion habitacion = db.habitaciones.Find(id);
        //    db.habitaciones.Remove(habitacion);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        //**********************************************************


        public byte[] ImageToArray(Image imagen)
        {
            MemoryStream ms = new MemoryStream();
            imagen.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public Image ArrayToImage(byte[] ArrBite)
        {
            MemoryStream ms = new MemoryStream(ArrBite);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        //public ActionResult Ver(int id)
        //{
        //    var habitacion = db.habitaciones.Single(x => x.HabitacionID == id);

        //    if (habitacion != null && habitacion.Image != null)
        //    {
        //        MemoryStream ms = new MemoryStream(habitacion.Image);
        //        return new FileStreamResult(ms, "image/jpeg");
        //    }
        //    else
        //    {
        //        // en caso de q no tenga imagen, tomo una imagen alternativa
        //        MemoryStream stream = new MemoryStream();
        //        var path = Server.MapPath(@"/Content/images/image_alternativa.png");
        //        var image = new System.Drawing.Bitmap(path);
        //        image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
        //        stream.Seek(0, SeekOrigin.Begin);
        //        return new FileStreamResult(stream, "image/jpeg");
        //    }

        //}

        // mostrar todas las habitaciones

        public ActionResult Disponibles()
        {
            var habitaciones = db.Habitacion.ToList();
            return View(habitaciones);
        }

        //using (MemoryStream ms = new MemoryStream()) {
        //    file.InputStream.CopyTo(ms);
        //    byte[] array = ms.GetBuffer();
        //}

    }
}