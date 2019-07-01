using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParkingTicketFrontEnd.Controllers
{
    public class ParkingController : Controller
    {
        /// <summary>
        /// Use this to load the initial page
        /// </summary>
        /// <returns>Nothing this is a sample git</returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}