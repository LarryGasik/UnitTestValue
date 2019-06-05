using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingTicketLogic.DTO
{
    public class ScanInformation
    {
        public string Tag { get; set; }
        public ParkingOffense Offense { get; set; }
    }
}
