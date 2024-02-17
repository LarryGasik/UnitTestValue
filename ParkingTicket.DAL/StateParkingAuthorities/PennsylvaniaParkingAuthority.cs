using ParkingTicket.DataAccess.DTO;

namespace ParkingTicket.DataAccess.StateParkingAuthorities;

public class PennsylvaniaParkingAuthority : IStateParkingAuthority
{
    public List<ParkingTicketDto> GetTicketsFromTag(string tag)
    {
        //Note: I'm not actually hitting a service, so I'm doing this
        //      to simulate the HTTP request waiting. Sometimes fast.
        //      Sometimes Slow.
        Thread.Sleep((int)DateTime.Now.DayOfWeek * 777);
        var tickets = new List<ParkingTicketDto>();

        if (tag == "Alex")
            tickets.Add(new ParkingTicketDto
            {
                DateOfOffense = DateTime.MinValue, Fine = 26, Offense = "Two Hour Limit", State = "PN",
                TicketID = Guid.Empty
            });
        return tickets;
    }
}