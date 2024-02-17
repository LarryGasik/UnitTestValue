using ParkingTicket.DataAccess;
using ParkingTicket.DataAccess.DTO;

namespace ParkingTicket.DAL;

public class MyStateParkingAuthority : IMyStateParkingAuthority, IStateParkingAuthority
{
    /// <summary>
    ///     In theory, this will create a new ticket in my state.
    /// </summary>
    /// <param name="offense">what did they do?</param>
    /// <param name="fine">What is the total Fine</param>
    /// <returns>DTO representation of the parking Ticket</returns>
    public ParkingTicketDto IssueParkingTicketDto(string offense, int fine)
    {
        return new ParkingTicketDto
            { DateOfOffense = DateTime.Now, Fine = 30, Offense = offense, State = "XX", TicketID = Guid.NewGuid() };
    }

    /// <summary>
    ///     Go to the Database for my state, and get all of the tickets for this car
    /// </summary>
    /// <param name="tag"> tag of car</param>
    /// <returns>A list of tickets for said car.</returns>
    public List<ParkingTicketDto> GetTicketsFromTag(string tag)
    {
        return new List<ParkingTicketDto>();
    }
}