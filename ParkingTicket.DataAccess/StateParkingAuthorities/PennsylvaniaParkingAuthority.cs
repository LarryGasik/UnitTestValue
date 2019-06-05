using System;
using System.Collections.Generic;
using ParkingTicket.DataAccess.DTO;

namespace ParkingTicket.DataAccess.StateParkingAuthorities
{
    public class PennsylvaniaParkingAuthority : IStateParkingAuthority
    {
        public List<ParkingTicketDto> GetTicketsFromTag(string tag)
        {
            List<ParkingTicketDto> tickets =  new List<ParkingTicketDto>();

            if (tag == "Alex")
            {
                tickets.Add(new ParkingTicketDto{DateOfOffense = DateTime.MinValue, Fine = 26, Offense = "Two Hour Limit", State = "PN", TicketID = Guid.Empty});                
            }
            return tickets;
        }
    }
}
