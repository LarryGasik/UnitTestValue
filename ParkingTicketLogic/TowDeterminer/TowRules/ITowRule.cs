using System.Collections.Generic;
using ParkingTicket.DataAccess.DTO;

namespace ParkingTicketLogic.TowDeterminer.TowRules
{
    public interface ITowRule
    {
        bool ShouldTowCar(List<ParkingTicketDto> existingTickets, ParkingOffense offense);
    }
}