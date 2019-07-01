using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ParkingTicketLogic;

namespace ParkingTicketFrontEnd.Controllers
{
    public class ParkingController : Controller
    {
        /// <summary>
        /// I ripped off this code from 
        /// https://www.codeproject.com/Articles/1056011/Bind-Enum-to-DropdownList-in-ASP-NET-MVC
        /// To create a quick and dirty means to pull enumerations into the UI 
        /// without too much fuss or violating the DRY principle.
        /// ejw 1-JLY-2019
        /// </summary>
        /// <returns>Nothing this is a sample git</returns>
        [HttpGet]
        public ActionResult Index()
        {
           //a bit of a hack but this will work
           ViewBag.OffenseList = from ParkingOffense e in Enum.GetValues(typeof(ParkingOffense))
                                  select new
                                  {
                                      ID = (int)e,
                                      Name = e.ToString()
                                  };
            //set up the form
            return View();
        }
    }
}