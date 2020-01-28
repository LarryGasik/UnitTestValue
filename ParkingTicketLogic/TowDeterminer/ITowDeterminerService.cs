using System.Collections.Generic;
using System.Threading.Tasks;
using ParkingTicket.DataAccess.DTO;

namespace ParkingTicketLogic.TowDeterminer
{
    public interface ITowDeterminerService
    {
        Task<bool> ShouldTowCar(ParkingOffense offense, string tag, int zipCode);
    }
}
