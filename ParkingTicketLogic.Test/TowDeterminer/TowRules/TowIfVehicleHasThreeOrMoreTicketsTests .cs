using System.Collections.Generic;
using NUnit.Framework;
using ParkingTicket.DataAccess.DTO;
using ParkingTicketLogic.TowDeterminer.TowRules;

namespace ParkingTicketLogic.Test.TowDeterminer.TowRules
{
    [TestFixture()]
    public class TowIfVehicleHasThreeOrMoreTicketsTests
    {
        private TowIfVehicleHasThreeOrMoreTickets _sut;

        [Test]
        public void DoNotTowCarsFor2Tickets()
        {
            _sut = new TowIfVehicleHasThreeOrMoreTickets(2);
            var result = _sut.ShouldTowCar();
            Assert.IsFalse(result);
        }

        [Test]
        public void TowCarsFor3Tickets()
        {
            _sut = new TowIfVehicleHasThreeOrMoreTickets(3);
            var result = _sut.ShouldTowCar();
            Assert.IsTrue(result);
        }

        [Test]
        public void TowCarsFor4Tickets()
        {
            _sut = new TowIfVehicleHasThreeOrMoreTickets(4);
            var result = _sut.ShouldTowCar();
            Assert.IsTrue(result);
        }
    }
}
