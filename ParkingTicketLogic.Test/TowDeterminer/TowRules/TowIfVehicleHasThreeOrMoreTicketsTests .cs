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
    public class TowIfVehicleHasThreeOrMoreTicketsTests
    {
        private TowIfVehicleHasThreeOrMoreTickets _sut;
        [SetUp]
        public void Setup()
        {
            _sut = new TowIfVehicleHasThreeOrMoreTickets();
        }

        [Test]
        public void DoNotTowCarsFor2Tickets()
        {
            List<ParkingTicketDto> tickets = new List<ParkingTicketDto>();
            tickets.Add(new ParkingTicketDto { Fine = 10000 });
            tickets.Add(new ParkingTicketDto { Fine = 10000 });
            var result = _sut.ShouldTowCar(tickets, ParkingOffense.BlockingFireHydrant);
            Assert.IsFalse(result);
        }
        [Test]
        public void TowCarsFor3Tickets()
        {
            List<ParkingTicketDto> tickets = new List<ParkingTicketDto>();
            tickets.Add(new ParkingTicketDto { Fine = 1 });
            tickets.Add(new ParkingTicketDto { Fine = 1 });
            tickets.Add(new ParkingTicketDto { Fine = 1 });
            var result = _sut.ShouldTowCar(tickets, ParkingOffense.BlockingFireHydrant);
            Assert.IsTrue(result);
        }
    }
}
