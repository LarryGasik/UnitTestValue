using ParkingTicket.DataAccess.DTO;

namespace ParkingTicket.DataAccess;

public interface IStateParkingAuthority
{
    //MASTER! MASTER! 1149
    List<ParkingTicketDto> GetTicketsFromTag(string tag);
}