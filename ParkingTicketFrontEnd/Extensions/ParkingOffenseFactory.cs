using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ParkingTicketLogic;

namespace ParkingTicketFrontEnd.Extensions
{
    /// <summary>
    /// This is my attempt to use generics in order to make it possible 
    /// to pass any type of enumereation into the and then generate a 
    /// select list from it.  
    /// https://codereview.stackexchange.com/questions/116656/extension-method-to-list-enum-values
    /// I ripped off the code from above. 3-July-2019
    /// <code>
    /// EnumerationExtensionMethods.SelectListFor(typeof(ParkingOffense));
    /// </code>
    /// This is how you would call the code below in an MVC Controler
    /// </summary>
    public static class EnumerationExtensionMethods
    {
        public static SelectList SelectListFor(Type enumType)
        {
            SelectList _outbound = null;

            if (enumType.IsEnum)
            {
                var queryValues = Enum.GetValues(enumType)
                    .Cast<int>()
                    .Where(i => !i.Equals(0))
                    .Select(e => new SelectListItem()
                    {
                        Value = e.ToString(),
                        Text = Enum.GetName(enumType, e).ToString()
                    }).ToList();

                _outbound = new SelectList(queryValues, "Value", "Text");

                return _outbound;
            }

            return null;

        }
    }
}