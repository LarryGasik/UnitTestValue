﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ParkingTicket.DataAccess;
using ParkingTicket.DataAccess.DTO;
using ParkingTicket.DataAccess.StateParkingAuthorities;
using ParkingTicketLogic.DTO;
using ParkingTicketLogic.Generators;
using ParkingTicketLogic.Providers;
using ParkingTicketLogic.TowDeterminer;

namespace ParkingTicketLogic
{
    public class ParkingTicketCalculator
    {
        private IHolidayService _holidayService;
        private ITicketGenerator _ticketGenerator;
        private ITowDeterminerService _towDeterminerService;
        public ParkingTicketCalculator() : this(new HolidaySerivice(), new TicketGenerator(), new TowDeterminerService())
        {

        }

        public ParkingTicketCalculator(IHolidayService holidayService, ITicketGenerator ticketGenerator, 
            ITowDeterminerService towDeterminerService)
        {
            _holidayService = holidayService;
            _ticketGenerator = ticketGenerator;
            _towDeterminerService = towDeterminerService;
        }

        /// <summary>
        /// Simulates a submission for an offense, and will spit out some sort of string that can be printed out,
        /// and tell the parking attendant if the car should get a ticket, be towed, etc.
        /// </summary>
        /// <param name="scan">Information about the car with the offense</param>
        /// <returns>String indicating what should be done to the car</returns>
        public string ScanForOffense(ScanInformation scan)
        {
            List<ParkingTicketDto>ParkingTickets = new List<ParkingTicketDto>();
            
            //Is this a valid parking offense?
            IMyStateParkingAuthority myState= new MyStateParkingAuthority();
            
            //Is It a holiday?
            var holidays = _holidayService.GetHolidays();
            bool isHoliday = holidays.Any(x => x.Date.Month == SystemTime.Now().Month && x.Date.Day == SystemTime.Now().Day);


            bool isTicketableOffense = true;
            if (isHoliday && scan.Offense == ParkingOffense.ExpiredParkingMeter)
            {
                //It is a holiday, we don't charge meters on holiday!
                isTicketableOffense = false;
            }

            //We don't want to give a ticket to the same tag, on the same day, for the same thing
            IStateParkingAuthority MY = new MyStateParkingAuthority();
            List<ParkingTicketDto> myStateParkingTickets = MY.GetTicketsFromTag(scan.Tag);
            if (myStateParkingTickets.Any(x=>x.Offense==scan.Offense.ToString() && x.DateOfOffense==DateTime.Now))
            {
                isTicketableOffense = false;
            }

            //We Determined they need a parking ticket.
            if (isTicketableOffense)
            {
                ParkingTickets.Add(myState.IssueParkingTicketDto(scan.Offense.ToString(),30));
            }

            ParkingTickets.Add(myState.IssueParkingTicketDto(scan.Offense.ToString(), 30));

            //Does this car need to be towed?
            //We tow cars when they have 3 or more tickets, or owe $300 to the collective parking authorities.
            IStateParkingAuthority IL = new IllinoisParkingAuthority();
            IStateParkingAuthority IN = new IndianaParingAuthority();
            IStateParkingAuthority PA = new PennsylvaniaParkingAuthority();

            //Imagine if we did every state, and each called a web service.
            ParkingTickets.AddRange(MY.GetTicketsFromTag(scan.Tag));
            ParkingTickets.AddRange(IL.GetTicketsFromTag(scan.Tag));
            ParkingTickets.AddRange(IN.GetTicketsFromTag(scan.Tag));
            ParkingTickets.AddRange(PA.GetTicketsFromTag(scan.Tag));

            bool towCar = false;
            towCar = _towDeterminerService.ShouldTowCar(ParkingTickets, scan.Offense);
           
            string result = _ticketGenerator.InstructionGenerator(towCar, isTicketableOffense);
            
            return result;
            
        }
    }
}
