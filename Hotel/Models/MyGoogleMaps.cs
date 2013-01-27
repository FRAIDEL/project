using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotel.Models
{
    public class MyGoogleMaps
    {
        public string SiteName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string InfoWindow { get; set; }
    }

    // CLASE DEL REPOSITORIO
    public class MarkerRepository
    {

        public IList<MyGoogleMaps> GetMarkers()
        {
            var MyGoogleMapss = new List<MyGoogleMaps>
                                    {
                                        new MyGoogleMaps
                                            {
                                                SiteName = "MY HOSME",
                                                Latitude = 9.404373557368585,
                                                Longitude = -75.6704592704773,
                                                InfoWindow = "HOTEL ESTELA DEL MAR :)"
                                            },
                                        //new MyGoogleMaps
                                        //    {
                                        //        SiteName = "Campo del Moro",
                                        //        Latitude = 40.419658,
                                        //        Longitude = -3.718801,
                                        //        InfoWindow = "InfoWindow del Campo del Moro"
                                        //    },
                                        //new MyGoogleMaps
                                        //    {
                                        //        SiteName = "Parque de la Cornisa",
                                        //        Latitude = 40.413254,
                                        //        Longitude = -3.716483,
                                        //        InfoWindow = "InfoWindow del Parque de la Cornisa"
                                        //    }
                                    };

            return MyGoogleMapss;
        }
    }
}