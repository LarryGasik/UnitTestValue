using ParkingTicket.DAL;
using ParkingTicket.DataAccess;
using ParkingTicket.DataAccess.DTO;
using ParkingTicket.DataAccess.StateParkingAuthorities;
using ParkingTicket.Logging;
using ParkingTicket.Logic.TowDeterminer.TowRuleEnforcements;
using ParkingTicketLogic;
using ParkingTicketLogic.TowDeterminer.TowRuleEnforcements;

namespace ParkingTicket.Logic.TowDeterminer;

/// <summary>
///     This class will determine if a car should be towed or not
/// </summary>
public class TowDeterminerService : ITowDeterminerService
{
    private readonly ITowRuleEnforcements _EnforcementRules;
    private readonly ILogger _logger;
    private readonly IEnumerable<IStateParkingAuthority> _parkingAuthorities;

    public TowDeterminerService(
        ILogger logger,
        IEnumerable<IStateParkingAuthority> parkingAuthorities,
        ITowRuleEnforcements rules)
    {
        _logger = logger;
        _parkingAuthorities = parkingAuthorities;
        _EnforcementRules = rules;
    }

    public bool ShouldTowCar(ParkingOffense offense, string tag, int zipCode)
    {
        var ParkingTickets = new List<ParkingTicketDto>();

        //Note: Imagine if we did all 50 states, and each called a web service.
        //Todo: We can eventually move this to async calls
        //Todo: Let's see if we can reduce the number of calls
        //      by changing how we add to the parking tickets object.
        //      Once we hit one state that trips flags for being towed,
        //      no need to keep calling.
        foreach (var parkingAuthority in _parkingAuthorities)
            try
            {
                ParkingTickets.AddRange(parkingAuthority.GetTicketsFromTag(tag));
            }
            catch (Exception e)
            {
                _logger.LogException(e);
            }

        var shouldTow = _EnforcementRules.ShouldTowCar(ParkingTickets, offense, zipCode);
        return shouldTow;
    }
}