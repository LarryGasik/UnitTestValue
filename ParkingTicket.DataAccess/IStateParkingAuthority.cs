using System.Collections.Generic;
using System.Threading.Tasks;
using ParkingTicket.DataAccess.DTO;

namespace ParkingTicket.DataAccess
{
    public interface IStateParkingAuthority
    {
        Task<List<ParkingTicketDto>> GetTicketsFromTag(string tag);
    }
}
