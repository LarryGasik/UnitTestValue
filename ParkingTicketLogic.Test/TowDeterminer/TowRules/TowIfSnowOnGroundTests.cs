using Moq;
using NUnit.Framework;
using ParkingTicket.DataAccess;
using ParkingTicketLogic.TowDeterminer.TowRules;

namespace ParkingTicketLogic.Test.TowDeterminer.TowRules
{
    [TestFixture()]
    public class TowIfSnowOnGroundTests
    {
        private TowIfSnowOnGround _sut;
        private Mock<IWeatherService> _weatherService;

        [SetUp]
        public void SetUp()
        {
            _weatherService = new Mock<IWeatherService>();
        }

        [Test]
        public void TowCarIfSnowInZip()
        {
            _weatherService.Setup(x => x.IsSnowOnTheGroundByZip(60606)).Returns(true);
            _sut = new TowIfSnowOnGround(_weatherService.Object,60606);
            var result = _sut.ShouldTowCar();
            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldNotTowCarIfNoSnow()
        {
            _weatherService.Setup(x => x.IsSnowOnTheGroundByZip(60606)).Returns(false);
            _sut = new TowIfSnowOnGround(_weatherService.Object, 60606);
            var result = _sut.ShouldTowCar();
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldCallWeatherServiceForZip()
        {
            _sut = new TowIfSnowOnGround(_weatherService.Object, 60606);
            var result = _sut.ShouldTowCar();
            _weatherService.Verify(x=>x.IsSnowOnTheGroundByZip(60606), Times.Once);
        }
    }
}
