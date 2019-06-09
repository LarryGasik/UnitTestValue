using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingTicket.DataAccess.DTO;
using ParkingTicketLogic.TowDeterminer.TowRules;

namespace ParkingTicketLogic.TowDeterminer.TowRuleEnforcements
{
    public class TowRuleEnforcementsSpring2019:ITowRuleEnforcements
    {
        public bool ShouldTowCar(List<ParkingTicketDto> existingTickets, ParkingOffense offense)
        {
            //Note: We're doing it this way to not invalidate the open/close principle. 
            //      There's no business logic here to test though, since it is just a list, 
            //      and the business logic is inside the rules.
            List<ITowRule> towRules = new List<ITowRule>();
            towRules.Add(new TowIfInHandicappedSpot());
            towRules.Add(new TowIfTotalFinesEquateMoreThanMaximumAmount());
            towRules.Add(new TowIfVehicleHasThreeOrMoreTickets());
            //Don't need a tow rule for parking with 2" of snow

            //Todo: Let's look into a better way of handling ShouldTowCar. I don't
            //      like how it takes in objects it doesn't need every time. Move to abstract?
            bool shouldTow = towRules.Any(x =>x.ShouldTowCar(existingTickets, offense));
            return shouldTow;
        }
    }
}
