using System.Collections.Generic;
using System.Linq;
using ParkingTicket.DataAccess.DTO;
using ParkingTicketLogic.TowDeterminer.TowRules;

namespace ParkingTicketLogic.TowDeterminer.TowRuleEnforcements
{
    public class TowRuleEnforcementsSpring2019:ITowRuleEnforcements
    {
        public bool ShouldTowCar(List<ParkingTicketDto> existingTickets, ParkingOffense offense, int zipCode)
        {
            //Note: In the spring, we really don't care about the zip code.
            //      It felt silly thought to create an overload to just point
            //      to another method.

            //Note: We're doing it this way to not invalidate the open/close principle. 
            //      There's no business logic here to test though, since it is just a list, 
            //      and the business logic is inside the rules.
            List<TowRule> towRules = new List<TowRule>();
            towRules.Add(new TowIfInHandicappedSpot(offense));
            towRules.Add(new TowIfTotalFinesEquateMoreThanMaximumAmount(existingTickets.Sum(x=>x.Fine)));
            towRules.Add(new TowIfVehicleHasThreeOrMoreTickets(existingTickets.Count));

            bool shouldTow = towRules.Any(x =>x.ShouldTowCar());
            return shouldTow;
        }
    }
}
