using ParkingTicket.DataAccess;
using ParkingTicketLogic.DTO;
using ParkingTicketLogic.Generators;
using ParkingTicketLogic.TowDeterminer;

namespace ParkingTicketLogic
{
    public class ParkingTicketCalculator
    {
        private IHolidayService _holidayService;
        private ITicketGenerator _ticketGenerator;
        private ITowDeterminerService _towDeterminerService;
        private ITicketIssuer _TicketDeterminer;
        public ParkingTicketCalculator() : this(new TicketIssuer(), new TicketGenerator(), new TowDeterminerService())
        {
        }

        public ParkingTicketCalculator(ITicketIssuer ticketDeterminer, ITicketGenerator ticketGenerator, 
            ITowDeterminerService towDeterminerService)
        {
            _ticketGenerator = ticketGenerator;
            _towDeterminerService = towDeterminerService;
            _TicketDeterminer = ticketDeterminer;
        }

        /// <summary>
        /// Simulates a submission for an offense, and will spit out some sort of string that can be printed out,
        /// and tell the parking attendant if the car should get a ticket, be towed, etc.
        /// </summary>
        /// <param name="scan">Information about the car with the offense</param>
        /// <returns>String indicating what should be done to the car</returns>
        public string ScanForOffense(ScanInformation scan)
        {
            bool issueTicket = _TicketDeterminer.DetermineTicket(scan.Offense, scan.Tag);
            bool towCar = _towDeterminerService.ShouldTowCar(scan.Offense, scan.Tag);
            string result = _ticketGenerator.InstructionGenerator(towCar, issueTicket);
            return result;
            
        }
    }
}
