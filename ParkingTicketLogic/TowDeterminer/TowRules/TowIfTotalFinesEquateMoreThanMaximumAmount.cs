using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingTicket.DataAccess.DTO;

namespace ParkingTicketLogic.TowDeterminer.TowRules
{
    public class TowIfTotalFinesEquateMoreThanMaximumAmount : ITowRule
    {
        public bool ShouldTowCar(List<ParkingTicketDto> existingTickets, ParkingOffense offense)
        {
            //Todo: Retrieve Maximum from a config, or some other place
            return existingTickets.Sum(x => x.Fine) > 300;
        }
    }
}
