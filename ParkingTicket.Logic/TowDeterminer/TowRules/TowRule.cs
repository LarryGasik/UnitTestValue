using System.Collections.Generic;
using ParkingTicket.DataAccess.DTO;

namespace ParkingTicketLogic.TowDeterminer.TowRules
{
    public abstract class TowRule
    {
       public abstract bool ShouldTowCar();
    }
}