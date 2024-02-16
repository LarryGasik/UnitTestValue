﻿using ParkingTicket.DAL;
using ParkingTicket.DataAccess;
using ParkingTicket.DataAccess.DTO;
using ParkingTicket.DataAccess.StateParkingAuthorities;
using ParkingTicket.Logging;
using ParkingTicketLogic;
using ParkingTicketLogic.TowDeterminer;
using ParkingTicketLogic.TowDeterminer.TowRuleEnforcements;

namespace ParkingTicket.Logic.TowDeterminer
{
    /// <summary>
    /// This class will determine if a car should be towed or not
    /// </summary>
    public class TowDeterminerService:ITowDeterminerService
    {
        private ILogger _logger;
        private IStateParkingAuthority _MY;
        private IStateParkingAuthority _IL;
        private IStateParkingAuthority _IN;
        private IStateParkingAuthority _PA;
        private ITowRuleEnforcements _EnforcementRules;

        //Todo: This is too messy. Let's take advantage of UnityContainer
        //      Because these constructors are out of control. Can we use 
        //      It in just a library?
        public TowDeterminerService():this(
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
            List<ParkingTicketDto> ParkingTickets = new List<ParkingTicketDto>();
            
            //Gather Tickets from all states
            List<IStateParkingAuthority> parkingAuthorities = new List<IStateParkingAuthority>
            {
                _MY, _IL, _IN, _PA
            };

            //Note: Imagine if we did all 50 states, and each called a web service.
            //Todo: We can eventually move this to async calls
            //Todo: Let's see if we can reduce the number of calls
            //      by changing how we add to the parking tickets object.
            //      Once we hit one state that trips flags for being towed,
            //      no need to keep calling.
            foreach (IStateParkingAuthority parkingAuthority in parkingAuthorities)
            {
                try
                {
                    ParkingTickets.AddRange(parkingAuthority.GetTicketsFromTag(tag));
                }
                catch (Exception e)
                {
                    _logger.LogException(e);
                }
            }

            bool shouldTow = _EnforcementRules.ShouldTowCar(ParkingTickets, offense, zipCode);
            return shouldTow;
        }
    }
}
