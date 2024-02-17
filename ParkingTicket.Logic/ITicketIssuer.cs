namespace ParkingTicketLogic;

public interface ITicketIssuer
{
    bool DetermineTicket(ParkingOffense scanOffense, string scanTag);
}