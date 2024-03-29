﻿using ParkingTicket.DAL;
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
    private readonly IStateParkingAuthority _IL;
    private readonly IStateParkingAuthority _IN;
    private readonly ILogger _logger;
    private readonly IStateParkingAuthority _MY;
    private readonly IStateParkingAuthority _PA;

    //Todo: This is too messy. Let's take advantage of UnityContainer
    //      Because these constructors are out of control. Can we use 
    //      It in just a library?
    public TowDeterminerService() : this(
        new Logger(),
        new MyStateParkingAuthority(),
        new IllinoisParkingAuthority(),
        new IndianaParingAuthority(),
        new PennsylvaniaParkingAuthority(),
        new TowRuleEnforcementsSpring2019())
    {
    }

    public TowDeterminerService(
        ILogger logger,
        IStateParkingAuthority MY,
        IStateParkingAuthority IL,
        IStateParkingAuthority IN,
        IStateParkingAuthority PA,
        ITowRuleEnforcements rules)
    {
        _logger = logger;
        _MY = MY;
        _IL = IL;
        _IN = IN;
        _PA = PA;
        _EnforcementRules = rules;
    }

    public bool ShouldTowCar(ParkingOffense offense, string tag, int zipCode)
    {
        var ParkingTickets = new List<ParkingTicketDto>();

        //Gather Tickets from all states
        var parkingAuthorities = new List<IStateParkingAuthority>
        {
            _MY, _IL, _IN, _PA
        };

        //Note: Imagine if we did all 50 states, and each called a web service.
        //Todo: We can eventually move this to async calls
        //Todo: Let's see if we can reduce the number of calls
        //      by changing how we add to the parking tickets object.
        //      Once we hit one state that trips flags for being towed,
        //      no need to keep calling.
        foreach (var parkingAuthority in parkingAuthorities)
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