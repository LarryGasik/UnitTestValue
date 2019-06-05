using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingTicketLogic.Test.DTO;

namespace ParkingTicketLogic.Test.StateParkingAuthorities
{
    public class IndianaParingAuthority:IStateParkingAuthority
    {
        public List<ParkingTicketDto> GetTicketsFromTag(string tag)
        {
            List<ParkingTicketDto> tickets =  new List<ParkingTicketDto>();
            return tickets;
        }
    }
}
