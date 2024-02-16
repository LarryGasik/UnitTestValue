using System.Collections.Generic;
using ParkingTicket.DataAccess.DTO;

namespace ParkingTicket.DataAccess
{
    public interface IStateParkingAuthority
    {
        List<ParkingTicketDto> GetTicketsFromTag(string tag);
    }
}
