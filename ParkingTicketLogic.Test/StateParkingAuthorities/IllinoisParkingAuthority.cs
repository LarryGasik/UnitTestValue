﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingTicketLogic.Test.DTO;

namespace ParkingTicketLogic.Test.StateParkingAuthorities
{
    public class IllinoisParkingAuthority:IStateParkingAuthority
    {
        public List<ParkingTicketDto> GetTicketsFromTag(string tag)
        {
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
