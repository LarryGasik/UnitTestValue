using System.Collections.Generic;
using ParkingTicket.DataAccess.DTO;

namespace ParkingTicketLogic.TowDeterminer
{
    public interface ITowDeterminerService
    {
        bool ShouldTowCar(List<ParkingTicketDto> tickets, ParkingOffense offense);
    }
}
