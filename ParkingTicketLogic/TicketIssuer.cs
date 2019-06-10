using System.Collections.Generic;
using System.Linq;
using ParkingTicket.DataAccess;
using ParkingTicket.DataAccess.DTO;
using ParkingTicketLogic.Providers;

namespace ParkingTicketLogic
{
    public class TicketIssuer:ITicketIssuer
    {
        private IHolidayService _holidayService;
        private IMyStateParkingAuthority _myStateParkingAuthority;

        public TicketIssuer() : this(new HolidaySerivice(),new MyStateParkingAuthority())
        {
        }

        public TicketIssuer(IHolidayService holidayService, IMyStateParkingAuthority myStateParkingAuthority)
        {
            _holidayService = holidayService;
            _myStateParkingAuthority = myStateParkingAuthority;
        }
        public bool DetermineTicket(ParkingOffense scanOffense, string scanTag)
        {
            //Note: We should probably move this into an engine like we have for tows.
            //      It could also determine how much the ticket is for, and handle
            //      warning tickets maybe.
            bool isTicketableOffense = true;
            if (scanOffense == ParkingOffense.ExpiredParkingMeter)
            {
                //Is It a holiday?
                var holidays = _holidayService.GetHolidays();
                bool isHoliday = holidays.Any(
                    x => x.Date.Month == SystemTime.Now().Month
                         && x.Date.Day == SystemTime.Now().Day);

                //It is a holiday, we don't charge meters on holiday!
                isTicketableOffense = !isHoliday;
            }

            //We don't want to give a ticket to the same tag, on the same day, for the same thing
            List<ParkingTicketDto> myStateParkingTickets = _myStateParkingAuthority.GetTicketsFromTag(scanTag);
            if (myStateParkingTickets.Any(x => 
                x.Offense == scanOffense.ToString() 
                && x.DateOfOffense.Month == SystemTime.Now().Month
                && x.DateOfOffense.Day == SystemTime.Now().Day
                && x.DateOfOffense.Year == SystemTime.Now().Year
                ))
            {
                isTicketableOffense = false;
            }

            //We Determined they need a parking ticket.
            if (isTicketableOffense)
            {
                _myStateParkingAuthority.IssueParkingTicketDto(scanOffense.ToString(), 30);
            }
            return isTicketableOffense;
        }
    }
}
