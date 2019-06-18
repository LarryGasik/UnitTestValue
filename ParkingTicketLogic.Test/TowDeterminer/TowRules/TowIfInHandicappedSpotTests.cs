using NUnit.Framework;
using ParkingTicketLogic.TowDeterminer.TowRules;

namespace ParkingTicketLogic.Test.TowDeterminer.TowRules
{
    [TestFixture()]
    public class TowIfInHandicappedSpotTests
    {
        private TowIfInHandicappedSpot _sut;

        [Test]
        public void TowCarsIfInHandicappedSpot()
        {
            _sut = new TowIfInHandicappedSpot(ParkingOffense.HandicappedParkingSpot);
            var result = _sut.ShouldTowCar();
            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldNotTowIfExpiredMeter()
        {
            _sut = new TowIfInHandicappedSpot(ParkingOffense.ExpiredParkingMeter);
            var result = _sut.ShouldTowCar();
            Assert.IsFalse(result);
        }
    }
}
