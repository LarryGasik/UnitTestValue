using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingTicketFrontEnd.Models
{
    public class ParkingTicket_VM
    {
        public string ZipCode { get; set; }
        public string VehicleTag { get; set; }
        public Int32 OffenseCode { get; set; }
        public string ParkingTicketMessage { get; set; }

        public ParkingTicket_VM()
        {
            ZipCode = string.Empty;
            VehicleTag = string.Empty;
            OffenseCode = 0;
            ParkingTicketMessage = string.Empty;
        }

    }
}