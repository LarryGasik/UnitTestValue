using System.Collections.Generic;
using ParkingTicketLogic.Test.DTO;

namespace ParkingTicketLogic.Test
{
    public interface IStateParkingAuthority
    {
        List<ParkingTicketDto> GetTicketsFromTag(string tag);
    }
}
