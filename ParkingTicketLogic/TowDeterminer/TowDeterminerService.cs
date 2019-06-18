using System.Collections.Generic;
using ParkingTicket.DataAccess;
using ParkingTicket.DataAccess.DTO;
using ParkingTicket.DataAccess.StateParkingAuthorities;
using ParkingTicketLogic.TowDeterminer.TowRuleEnforcements;

namespace ParkingTicketLogic.TowDeterminer
{
    /// <summary>
    /// This class will determine if a car should be towed or not
    /// </summary>
    public class TowDeterminerService:ITowDeterminerService
    {
        private IStateParkingAuthority _MY;
        private IStateParkingAuthority _IL;
        private IStateParkingAuthority _IN;
        private IStateParkingAuthority _PA;
        private ITowRuleEnforcements _EnforcementRules;

        //Todo: This is too messy. Let's take advantage of UnityContainer
        //      Because these constructors are out of control. Can we use 
        //      It in just a library?
        public TowDeterminerService():this(
            new MyStateParkingAuthority(),
            new IllinoisParkingAuthority(),
            new IndianaParingAuthority(),
            new PennsylvaniaParkingAuthority(),
            new TowRuleEnforcementsSpring2019())
        {

        }

        public TowDeterminerService(
            IStateParkingAuthority MY, 
            IStateParkingAuthority IL, 
            IStateParkingAuthority IN, 
            IStateParkingAuthority PA,
            ITowRuleEnforcements rules)
        {
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
            
            //Note: Imagine if we did every state, and each called a web service.
            //Todo: We can eventually move this to async calls
            //Todo: Let's see if we can reduce the number of calls
            //      by changing how we add to the parking tickets object.
            //      Once we hit one state that trips flags for being towed,
            //      no need to keep calling.
            ParkingTickets.AddRange(_MY.GetTicketsFromTag(tag));
            ParkingTickets.AddRange(_IL.GetTicketsFromTag(tag));
            ParkingTickets.AddRange(_IN.GetTicketsFromTag(tag));
            ParkingTickets.AddRange(_PA.GetTicketsFromTag(tag));

            bool shouldTow = _EnforcementRules.ShouldTowCar(ParkingTickets, offense, zipCode);
            return shouldTow;
        }
    }
}
