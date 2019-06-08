using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using ParkingTicket.DataAccess.DTO;
using ParkingTicketLogic.TowDeterminer.TowRules;

namespace ParkingTicketLogic.Test.TowDeterminer.TowRules
{
    [TestFixture()]
    public class TowIfTotalFinesEquateMoreThanMaximumAmountTests
    {
        private TowIfTotalFinesEquateMoreThanMaximumAmount _sut;
        [SetUp]
        public void Setup()
        {
            _sut = new TowIfTotalFinesEquateMoreThanMaximumAmount();
        }

        [Test]
        public void DoNotTowCarsIfTotalFineEqualToMax()
        {
            List<ParkingTicketDto> tickets = new List<ParkingTicketDto>();
            tickets.Add(new ParkingTicketDto{Fine=300});
            var result = _sut.ShouldTowCar(tickets, ParkingOffense.ExpiredParkingMeter);
            Assert.IsFalse(result);
        }

        [Test]
        public void TowCarsIfGreaterThanMax()
        {
            List<ParkingTicketDto> tickets = new List<ParkingTicketDto>();
            tickets.Add(new ParkingTicketDto { Fine = 301 });
            var result = _sut.ShouldTowCar(tickets, ParkingOffense.ExpiredParkingMeter);
            Assert.IsTrue(result);
        }

        [Test]
        public void TowCarsIfLessThanMax()
        {
            List<ParkingTicketDto> tickets = new List<ParkingTicketDto>();
            tickets.Add(new ParkingTicketDto { Fine = 299 });
            var result = _sut.ShouldTowCar(tickets, ParkingOffense.HandicappedParkingSpot);
            Assert.IsFalse(result);
        }
    }
}
