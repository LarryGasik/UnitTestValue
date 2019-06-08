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
    public class TowIfInHandicappedSpotTests
    {
        private TowIfInHandicappedSpot _sut;
        [SetUp]
        public void Setup()
        {
            _sut = new TowIfInHandicappedSpot();
        }

        [Test]
        public void TowCarsIfInHandicappedSpot()
        {
            var result = _sut.ShouldTowCar(new List<ParkingTicketDto>(), ParkingOffense.HandicappedParkingSpot);
            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldNotTowIfExpiredMeter()
        {
            List<ParkingTicketDto> tickets = new List<ParkingTicketDto>();
            tickets.Add( new ParkingTicketDto());
            tickets.Add( new ParkingTicketDto());
            tickets.Add( new ParkingTicketDto());
            tickets.Add( new ParkingTicketDto());
            var result = _sut.ShouldTowCar(tickets, ParkingOffense.ExpiredParkingMeter);
            Assert.IsFalse(result);
        }
    }
}
