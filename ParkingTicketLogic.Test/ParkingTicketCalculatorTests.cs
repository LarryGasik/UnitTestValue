using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ParkingTicket.DataAccess;
using ParkingTicket.DataAccess.DTO;
using ParkingTicketLogic.DTO;
using ParkingTicketLogic.Generators;
using ParkingTicketLogic.Providers;

namespace ParkingTicketLogic.Test
{
    [TestFixture]
    public class ParkingTicketCalculatorTests
    {
        private ParkingTicketCalculator _sut;
        private Mock<IHolidayService> _HolidayService;
        private Mock<ITicketGenerator> _TicketGenerator;

        [SetUp]
        public void SetUp()
        {
           _HolidayService = new Mock<IHolidayService>();
           _TicketGenerator = new Mock<ITicketGenerator>();
        }

        [Test]
        public void ExpiredMetersOnHolidayShouldNotIssueTicket()
        {
            SystemTime.SetDateTime(new DateTime(2019,05,22));
            _HolidayService.Setup(x => x.GetHolidays())
                .Returns(
                    new List<HolidayDTO>
                    {
                        new HolidayDTO{Date = new DateTime(2019,05,22),TitleOfDay = "Mayor's Birthday"},
                    });

            _sut = new ParkingTicketCalculator(_HolidayService.Object, _TicketGenerator.Object);

            _sut.ScanForOffense(new ScanInformation {Offense = ParkingOffense.ExpiredParkingMeter, Tag = "Tag"});
            _TicketGenerator.Verify(x => x.InstructionGenerator(It.IsAny<bool>(), false));
        }

        [Test]
        public void ExpiredMeterNotOnHolidayShouldIssueTicket()
        {
            SystemTime.SetDateTime(new DateTime(2019, 05, 24));
            _HolidayService.Setup(x => x.GetHolidays())
                .Returns(
                    new List<HolidayDTO>
                    {
                        new HolidayDTO{Date = new DateTime(2019,05,22),TitleOfDay = "Mayor's Birthday"},
                    });

            _sut = new ParkingTicketCalculator(_HolidayService.Object, _TicketGenerator.Object);

            _sut.ScanForOffense(new ScanInformation { Offense = ParkingOffense.ExpiredParkingMeter, Tag = "Tag" });
            _TicketGenerator.Verify(x => x.InstructionGenerator(It.IsAny<bool>(), true));
        }

        [Test]
        public void HandicappedOnHolidayShouldIssueTicket()
        {
            SystemTime.SetDateTime(new DateTime(2019, 05, 22));
            _HolidayService.Setup(x => x.GetHolidays())
                .Returns(
                    new List<HolidayDTO>
                    {
                        new HolidayDTO{Date = new DateTime(2019,05,22),TitleOfDay = "Mayor's Birthday"},
                    });

            _sut = new ParkingTicketCalculator(_HolidayService.Object, _TicketGenerator.Object);

            _sut.ScanForOffense(new ScanInformation { Offense = ParkingOffense.HandicappedParkingSpot, Tag = "Tag" });
            _TicketGenerator.Verify(x => x.InstructionGenerator(It.IsAny<bool>(), true));
        }
    }
}
