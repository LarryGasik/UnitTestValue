using ParkingTicket.Logic.TowDeterminer;
using ParkingTicketLogic;
using ParkingTicketLogic.DTO;
using ParkingTicketLogic.Generators;

namespace ParkingTicket.Logic;

public class ParkingTicketCalculator
{
    private readonly ITicketGenerator _ticketGenerator;
    private readonly ITicketIssuer _ticketIssuer;
    private readonly ITowDeterminerService _towDeterminerService;

    public ParkingTicketCalculator() : this(new TicketIssuer(), new TicketGenerator(), new TowDeterminerService())
    {
    }

    public ParkingTicketCalculator(ITicketIssuer ticketIssuer, ITicketGenerator ticketGenerator,
        ITowDeterminerService towDeterminerService)
    {
        _ticketGenerator = ticketGenerator;
        _towDeterminerService = towDeterminerService;
        _ticketIssuer = ticketIssuer;
    }

    /// <summary>
    ///     Simulates a submission for an offense, and will spit out some sort of string that can be printed out,
    ///     and tell the parking attendant if the car should get a ticket, be towed, etc.
    /// </summary>
    /// <param name="scan">Information about the car with the offense</param>
    /// <returns>String indicating what should be done to the car</returns>
    public string ScanForOffense(ScanInformation scan)
    {
        var issuedTicket = _ticketIssuer.DetermineTicket(scan.Offense, scan.Tag);
        var towCar = _towDeterminerService.ShouldTowCar(scan.Offense, scan.Tag, scan.zipCode);
        var result = _ticketGenerator.InstructionGenerator(towCar, issuedTicket);
        return result;
    }
}