using System;
using System.Collections.Generic;
using System.Threading;
using ParkingTicket.DataAccess.DTO;

namespace ParkingTicket.DataAccess.StateParkingAuthorities
{
    public class IllinoisParkingAuthority:IStateParkingAuthority
    {
        public List<ParkingTicketDto> GetTicketsFromTag(string tag)
        {
            //Note: I'm not actually hitting a service, so I'm doing this
            //      to simulate the HTTP request waiting. Sometimes fast.
            //      Sometimes Slow.
            Thread.Sleep(2000);

            List<ParkingTicketDto> tickets =  new List<ParkingTicketDto>();
            if (tag == "Larry")
            {
                tickets.Add(new ParkingTicketDto{DateOfOffense = DateTime.MinValue, Fine = 400, Offense = "Restricted Parking Zone", State = "IL", TicketID = Guid.Empty});                
            }
            if (tag == "Diesel")
            {
                tickets.Add(new ParkingTicketDto { DateOfOffense = DateTime.MinValue, Fine = 100, Offense = "Fire Hydrant", State = "IL", TicketID = Guid.Empty });
            }
            return tickets;
        }
    }
}
