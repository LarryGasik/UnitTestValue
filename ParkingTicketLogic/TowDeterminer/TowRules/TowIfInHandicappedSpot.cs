using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingTicket.DataAccess.DTO;

namespace ParkingTicketLogic.TowDeterminer.TowRules
{
    public class TowIfInHandicappedSpot:ITowRule
    {
        public bool ShouldTowCar(List<ParkingTicketDto> existingTickets, ParkingOffense offense)
        {
            return offense == ParkingOffense.HandicappedParkingSpot;
        }
    }
}
