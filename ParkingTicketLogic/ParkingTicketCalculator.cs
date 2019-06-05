using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ParkingTicketLogic.DTO;

namespace ParkingTicketLogic
{
    public class ParkingTicketCalculator
    {
        /// <summary>
        /// Simulates a submission for an offense, and will spit out some sort of string that can be printed out,
        /// and tell the parking attendant if the car should get a ticket, be towed, etc.
        /// </summary>
        /// <param name="scan">Information about the car with the offense</param>
        /// <returns>String indicating what should be done to the car</returns>
        public string ScanForOffense(ScanInformation scan)
        {
            //Is this a valid parking offense?

            //Does this car need to be towed?
            
            return String.Empty;
            
        }
    }
}
