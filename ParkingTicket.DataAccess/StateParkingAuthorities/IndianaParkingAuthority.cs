using System.Collections.Generic;
using ParkingTicket.DataAccess.DTO;

namespace ParkingTicket.DataAccess.StateParkingAuthorities
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
