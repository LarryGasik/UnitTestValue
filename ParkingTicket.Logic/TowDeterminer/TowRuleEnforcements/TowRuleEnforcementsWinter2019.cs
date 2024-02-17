using ParkingTicket.DataAccess.DTO;
using ParkingTicket.Logic.TowDeterminer.TowRules;
using ParkingTicketLogic;
using ParkingTicketLogic.TowDeterminer.TowRules;

namespace ParkingTicket.Logic.TowDeterminer.TowRuleEnforcements;

public class TowRuleEnforcementsWinter2019 : ITowRuleEnforcements
{
    //Todo: I feel like we should now figure out how to test this.
    //      Previously, I wasn't sure because it was just a 
    //      collection, but now that there's two different ones,
    //      I think it is time.
    public bool ShouldTowCar(List<ParkingTicketDto> existingTickets,
        ParkingOffense offense, int zipCode)
    {
        //Note: We're doing it this way to not invalidate the open/close principle. 
        //      There's no business logic here to test though, since it is just a list, 
        //      and the business logic is inside the rules.
        var towRules = new List<TowRule>();
        towRules.Add(new TowIfInHandicappedSpot(offense));
        towRules.Add(new TowIfTotalFinesEquateMoreThanMaximumAmount(existingTickets.Sum(x => x.Fine)));
        towRules.Add(new TowIfVehicleHasThreeOrMoreTickets(existingTickets.Count));
        towRules.Add(new TowIfSnowOnGround(zipCode));

        var shouldTow = towRules.Any(x => x.ShouldTowCar());
        return shouldTow;
    }
}