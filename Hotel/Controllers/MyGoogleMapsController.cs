using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hotel.Models;

namespace Hotel.Controllers
{
    public class MyGoogleMapsController : Controller
    {
        //
        // GET: /MyGoogleMaps/

        [HttpGet]
        public ActionResult Sync()
        {
            MarkerRepository marcked = new MarkerRepository();
            //return View(_markerRepository.GetMarkers());
            return View(marcked.GetMarkers());
        }

    }
}
