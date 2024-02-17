using ParkingTicket.DataAccess.DTO;
using ParkingTicketLogic;

namespace ParkingTicket.Logic.TowDeterminer.TowRuleEnforcements
{
    public interface ITowRuleEnforcements
    {
        bool ShouldTowCar(List<ParkingTicketDto> existingTickets, ParkingOffense offense, int zipCode);
    }
}