using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ParkingTicketLogic;

namespace ParkingTicketFrontEnd.Extensions
{
    public class ParkingOffenseFactory
    {
        /// <summary>
        /// welcome to the DRY example of not repeating yourself
        /// write the code once so that you have one place to go
        /// when something goes wrong. ejw 2-JLY-2019
        /// I ripped off this code from 
        /// https://www.codeproject.com/Articles/1056011/Bind-Enum-to-DropdownList-in-ASP-NET-MVC
        /// To create a quick and dirty means to pull enumerations into the UI 
        /// </summary>
        /// <returns>MVC Select List</returns>
        public static SelectList CreateOffenseSelectList()
        {
            SelectList _outbound;
            
            //a bit of a hack but this will work
            var offfenses = from ParkingOffense e in Enum.GetValues(typeof(ParkingOffense))
                                  select new
                                  {
                                      ID = (int)e,
                                      Name = e.ToString()
                                  };

            _outbound = new SelectList(offfenses, "ID", "Name");

            return _outbound;
        }
    }
}