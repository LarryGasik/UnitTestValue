using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingTicket.DataAccess.DTO;
using ParkingTicketLogic.TowDeterminer.TowRules;

namespace ParkingTicketLogic.TowDeterminer
{
    /// <summary>
    /// This class will determine if a car should be towed or not
    /// </summary>
    public class TowDeterminerService:ITowDeterminerService
    {
        public bool ShouldTowCar(List<ParkingTicketDto> tickets, ParkingOffense offense)
        {
            //Note: We're doing it this way to not invalidate the open/close principle. 
            //      There's no business logic here to test though, since it is just a list, 
            //      and the business logic is inside the rules.
            List<ITowRule> towRules = new List<ITowRule>();
            towRules.Add(new TowIfInHandicappedSpot());
            towRules.Add(new TowIfTotalFinesEquateMoreThanMaximumAmount());
            towRules.Add(new TowIfVehicleHasThreeOrMoreTickets());
            bool shouldTow = towRules.Any(x => true);
            return shouldTow;
        }
    }
}
