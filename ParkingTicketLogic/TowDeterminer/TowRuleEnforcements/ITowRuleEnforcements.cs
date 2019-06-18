using System.Collections.Generic;
using ParkingTicket.DataAccess.DTO;

namespace ParkingTicketLogic.TowDeterminer.TowRuleEnforcements
{
    public interface ITowRuleEnforcements
    {
        bool ShouldTowCar(List<ParkingTicketDto> existingTickets, ParkingOffense offense, int zipCode);
    }
}