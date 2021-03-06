﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingTicket.DataAccess.DTO;
using ParkingTicketLogic.TowDeterminer.TowRules;

namespace ParkingTicketLogic.TowDeterminer.TowRuleEnforcements
{
    public class TowRuleEnforcementsWinter2019:ITowRuleEnforcements
    {
        //Todo: I feel like we should now figure out how to test this.
        //      Previously, I wasn't sure because it was just a 
        //      collection, but now that there's two different ones,
        //      I think it is time.
        public bool ShouldTowCar(List<ParkingTicketDto> existingTickets, 
            ParkingOffense offense, int zipCode)
        {
            //Note: We're doing it this way to not invalidate the open/close principle. 
            //      There's no business logic here to test though, since it is just a list, 
            //      and the business logic is inside the rules.
            List<TowRule> towRules = new List<TowRule>();
            towRules.Add(new TowIfInHandicappedSpot(offense));
            towRules.Add(new TowIfTotalFinesEquateMoreThanMaximumAmount(existingTickets.Sum(x=>x.Fine)));
            towRules.Add(new TowIfVehicleHasThreeOrMoreTickets(existingTickets.Count));
            towRules.Add(new TowIfSnowOnGround(zipCode));

            bool shouldTow = towRules.Any(x =>x.ShouldTowCar());
            return shouldTow;
        }
    }
}
