using ParkingTicket.DataAccess.DTO;

namespace ParkingTicket.DAL;

public interface IMyStateParkingAuthority
{
    /// <summary>
    ///     Issuing a Parking ticket
    /// </summary>
    /// <param name="offense">What happened?</param>
    /// <param name="fine">How much is the fine?</param>
    /// <returns>DTO of the exampleTicket</returns>
    ParkingTicketDto IssueParkingTicketDto(string offense, int fine);

    List<ParkingTicketDto> GetTicketsFromTag(string tag);
}