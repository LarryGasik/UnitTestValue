using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingTicket.DataAccess.DTO;

namespace ParkingTicketLogic.TowDeterminer.TowRules
{
    public class TowIfVehicleHasThreeOrMoreTickets : ITowRule
    {
        public bool ShouldTowCar(List<ParkingTicketDto> existingTickets, ParkingOffense offense)
        {
            return existingTickets.Count >= 3;
        }
    }
}
