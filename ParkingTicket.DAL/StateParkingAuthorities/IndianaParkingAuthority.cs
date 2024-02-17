using ParkingTicket.DataAccess.DTO;

namespace ParkingTicket.DataAccess.StateParkingAuthorities;

public class IndianaParingAuthority : IStateParkingAuthority
{
    public List<ParkingTicketDto> GetTicketsFromTag(string tag)
    {
        //Note: I'm not actually hitting a service, so I'm doing this
        //      to simulate the HTTP request waiting. Sometimes fast.
        //      Sometimes Slow.
        Thread.Sleep(DateTime.Now.Second * 100);
        var tickets = new List<ParkingTicketDto>();
        return tickets;
    }
}