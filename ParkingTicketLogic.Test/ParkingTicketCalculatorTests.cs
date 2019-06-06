using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ParkingTicket.DataAccess;
using ParkingTicket.DataAccess.DTO;
using ParkingTicketLogic.DTO;
using ParkingTicketLogic.Providers;

namespace ParkingTicketLogic.Test
{
    [TestFixture]
    public class ParkingTicketCalculatorTests
    {
        private ParkingTicketCalculator _sut;
        private Mock<IHolidayService> _IHolidayService;

        [SetUp]
        public void SetUp()
        {
           _IHolidayService = new Mock<IHolidayService>();
        }

        [Test]
        public void TicketsShouldNotBeIssuedWhenItIsAHoliday()
        {
            SystemTime.SetDateTime(new DateTime(2019,05,22));
            _IHolidayService.Setup(x => x.GetHolidays())
                .Returns(
                    new List<HolidayDTO>
                    {
                        new HolidayDTO{Date = new DateTime(2019,05,22),TitleOfDay = "Mayor's Birthday"},
                    });
            _sut = new ParkingTicketCalculator(_IHolidayService.Object);
            string result = _sut.ScanForOffense(new ScanInformation {Offense = ParkingOffense.ExpiredParkingMeter, Tag = "Bill"});
            Assert.AreEqual(String.Empty, result);
            
        }

        [Test]
        public void TicketsShouldBeIssuedWhenItIsAHoliday()
        {
            SystemTime.SetDateTime(new DateTime(2019, 05, 21));
            _IHolidayService.Setup(x => x.GetHolidays())
                .Returns(
                    new List<HolidayDTO>
                    {
                        new HolidayDTO{Date = new DateTime(2019,05,22),TitleOfDay = "Mayor's Birthday"},
                    });
            _sut = new ParkingTicketCalculator(_IHolidayService.Object);
            string result = _sut.ScanForOffense(new ScanInformation { Offense = ParkingOffense.ExpiredParkingMeter, Tag = "Bill" });
            Assert.AreEqual("here's your ticket", result);

        }
    }
}
