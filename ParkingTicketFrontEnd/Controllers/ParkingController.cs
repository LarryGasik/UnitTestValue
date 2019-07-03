using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ParkingTicketFrontEnd.Extensions;
using ParkingTicketLogic;
using ParkingTicketFrontEnd.Models;

namespace ParkingTicketFrontEnd.Controllers
{
    public class ParkingController : Controller
    {
        /// <summary>
        /// without too much fuss or violating the DRY principle.
        /// ejw 2-JLY-2019
        /// </summary>
        /// <returns>Nothing this is a sample git</returns>
        [HttpGet]
        public ActionResult Index()
        {
            ParkingTicket_VM _VM = new ParkingTicket_VM();
            //application of the dry principle for the combo box.
            ViewBag.Offenses = EnumerationExtensionMethods.SelectListFor(typeof(ParkingOffense));
            //set up the form
            return View(_VM);
        }

        ///<summary>
        /// The HTTP post of the form
        /// ejw 2-JLY-2019 it is clear we are going 
        /// to needs a view model object to pass stuff back and forth
        /// 
        /// 
        /// </summary>
        [HttpPost]
        public ActionResult Index(string ZipCode, string VehicleTag, ParkingOffense cboOffense)
        {
            ParkingTicket_VM _VM = new ParkingTicket_VM();
            ParkingTicketCalculator _ticketCalculator = new ParkingTicketCalculator();
            int zip = Convert.ToInt32(ZipCode);
            //application of the dry principle for the combo box.
            ViewBag.Offenses = EnumerationExtensionMethods.SelectListFor(typeof(ParkingOffense));
            //this does the heavy lifting and tells us the message we need for the vehicle and tags.
            _VM.ParkingTicketMessage = _ticketCalculator.ScanForOffense(new ParkingTicketLogic.DTO.ScanInformation { Offense = cboOffense, Tag = VehicleTag, zipCode = zip });
            return View(_VM);
        }
    }
}