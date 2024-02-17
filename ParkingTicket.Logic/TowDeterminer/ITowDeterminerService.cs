using ParkingTicketLogic;

namespace ParkingTicket.Logic.TowDeterminer
{
    public interface ITowDeterminerService
    {
        bool ShouldTowCar(ParkingOffense offense, string tag, int zipCode);
    }
}
