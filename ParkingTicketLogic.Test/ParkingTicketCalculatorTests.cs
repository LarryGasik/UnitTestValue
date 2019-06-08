using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ParkingTicket.DataAccess;
using ParkingTicket.DataAccess.DTO;
using ParkingTicketLogic.DTO;
using ParkingTicketLogic.Generators;
using ParkingTicketLogic.Providers;
using ParkingTicketLogic.TowDeterminer;

namespace ParkingTicketLogic.Test
{
    [TestFixture]
    public class ParkingTicketCalculatorTests
    {
        private ParkingTicketCalculator _sut;
        private Mock<IHolidayService> _holidayService;
        private Mock<ITicketGenerator> _ticketGenerator;
        private Mock<ITowDeterminerService> _towDeterminerService;
        [SetUp]
        public void SetUp()
        {
           _holidayService = new Mock<IHolidayService>();
           _ticketGenerator = new Mock<ITicketGenerator>();
           _towDeterminerService=new Mock<ITowDeterminerService>();
        }

        [Test]
        public void ExpiredMetersOnHolidayShouldNotIssueTicket()
        {
            SystemTime.SetDateTime(new DateTime(2019,05,22));
            _holidayService.Setup(x => x.GetHolidays())
                .Returns(
                    new List<HolidayDTO>
                    {
                        new HolidayDTO{Date = new DateTime(2019,05,22),TitleOfDay = "Mayor's Birthday"},
                    });

            _sut = new ParkingTicketCalculator(_holidayService.Object, _ticketGenerator.Object,_towDeterminerService.Object);

            _sut.ScanForOffense(new ScanInformation {Offense = ParkingOffense.ExpiredParkingMeter, Tag = "Tag"});
            _ticketGenerator.Verify(x => x.InstructionGenerator(It.IsAny<bool>(), false));
        }

        [Test]
        public void ExpiredMeterNotOnHolidayShouldIssueTicket()
        {
            SystemTime.SetDateTime(new DateTime(2019, 05, 24));
            _holidayService.Setup(x => x.GetHolidays())
                .Returns(
                    new List<HolidayDTO>
                    {
                        new HolidayDTO{Date = new DateTime(2019,05,22),TitleOfDay = "Mayor's Birthday"},
                    });

            _sut = new ParkingTicketCalculator(_holidayService.Object, _ticketGenerator.Object, _towDeterminerService.Object);

            _sut.ScanForOffense(new ScanInformation { Offense = ParkingOffense.ExpiredParkingMeter, Tag = "Tag" });
            _ticketGenerator.Verify(x => x.InstructionGenerator(It.IsAny<bool>(), true));
        }

        [Test]
        public void HandicappedOnHolidayShouldIssueTicket()
        {
            SystemTime.SetDateTime(new DateTime(2019, 05, 22));
            _holidayService.Setup(x => x.GetHolidays())
                .Returns(
                    new List<HolidayDTO>
                    {
                        new HolidayDTO{Date = new DateTime(2019,05,22),TitleOfDay = "Mayor's Birthday"},
                    });

            _sut = new ParkingTicketCalculator(_holidayService.Object, _ticketGenerator.Object, _towDeterminerService.Object);

            _sut.ScanForOffense(new ScanInformation { Offense = ParkingOffense.HandicappedParkingSpot, Tag = "Tag" });
            _ticketGenerator.Verify(x => x.InstructionGenerator(It.IsAny<bool>(), true));
        }
    }
}
