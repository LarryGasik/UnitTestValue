using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingTicket.Logic;
using ParkingTicketLogic;
using ParkingTicketLogic.DTO;

namespace ParkingTicketUI
{
    class Program
    {
        static void Main(string[] args)
        {
            ParkingTicketCalculator ptc = new ParkingTicketCalculator();

            ParkingOffense myOffense = ParkingOffense.UnknownParkingOffense;
            //Todo: We have to validate user Input. We could pass ham as an argument
            //      and it shouldn't throw an exception. Maybe consider moving 
            //      the parsing to business logic so that we don't have to do this
            //      for the front end.
            if (Enum.IsDefined(typeof(ParkingOffense), Convert.ToInt32(args[1])))
            {
                myOffense = (ParkingOffense) Convert.ToInt32(args[1]);
            }


            string tag = args[0];

            //Todo: Probably should have verification on this so that we fail safely
            //      when someone enters ham as a zip.
            int zip = Convert.ToInt32(args[2]);
            Console.WriteLine("Parking Offense: " + myOffense.ToString());
            Console.WriteLine("Tag:             " + tag);
            Console.WriteLine("Zip Code:        " + zip);
            Console.WriteLine("Scan Started at: " + DateTime.Now.ToString());

            string result = ptc.ScanForOffense(new ScanInformation { Offense = myOffense, Tag = tag, zipCode = zip });
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine(result);
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Scan Done at:    " + DateTime.Now.ToString());
            Console.WriteLine("Press any key to close.");
            Console.ReadKey();
        }
    }
}
