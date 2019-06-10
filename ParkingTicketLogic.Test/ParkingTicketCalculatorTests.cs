using System;
using Moq;
using NUnit.Framework;
using ParkingTicketLogic.DTO;
using ParkingTicketLogic.Generators;
using ParkingTicketLogic.TowDeterminer;

namespace ParkingTicketLogic.Test
{
    [TestFixture]
    public class ParkingTicketCalculatorTests
    {
        private ParkingTicketCalculator _sut;
        private Mock<ITicketGenerator> _ticketGenerator;
        private Mock<ITicketIssuer> _ticketIssuer;
        private Mock<ITowDeterminerService> _towDeterminerService;
        private string tag;
        [SetUp]
        public void SetUp()
        {
            tag = Guid.NewGuid().ToString();
            _ticketGenerator = new Mock<ITicketGenerator>();
            _towDeterminerService=new Mock<ITowDeterminerService>();
            _ticketIssuer = new Mock<ITicketIssuer>();
        }

        [Test]
        public void ShouldTakeResultFromTicketIssuerAndSendToTicketGenerator()
        {
            //Arrange
            _ticketIssuer.Setup(x => x.DetermineTicket(ParkingOffense.BlockingFireHydrant, tag)).Returns(true);
            _sut = new ParkingTicketCalculator(_ticketIssuer.Object, _ticketGenerator.Object, _towDeterminerService.Object);

            //Act
            _sut.ScanForOffense(new ScanInformation {Offense = ParkingOffense.BlockingFireHydrant, Tag=tag});
            
            //Assert
            _ticketGenerator.Verify(x=>x.InstructionGenerator(It.IsAny<bool>(),true),Times.Once);
        }

        [Test]
        public void ShouldTakeResultFromTowGeneratorAndSendToTicketGenerator()
        {
            //Arrange
            _towDeterminerService.Setup(x => x.ShouldTowCar(ParkingOffense.BlockingFireHydrant, tag)).Returns(true);
            _sut = new ParkingTicketCalculator(_ticketIssuer.Object, _ticketGenerator.Object, _towDeterminerService.Object);

            //Act
            _sut.ScanForOffense(new ScanInformation { Offense = ParkingOffense.BlockingFireHydrant, Tag = tag });

            //Assert
            _ticketGenerator.Verify(x => x.InstructionGenerator(true, It.IsAny<bool>()), Times.Once);
        }

        [Test]
        public void ShouldReturnStringFromTicketGenerator()
        {
            string magicString = "Lemmy Rules";

            //Arrange
            _ticketGenerator.Setup(
                x => x.InstructionGenerator(It.IsAny<bool>(),It.IsAny<bool>()))
                .Returns(magicString);
            _sut = new ParkingTicketCalculator(_ticketIssuer.Object, _ticketGenerator.Object, _towDeterminerService.Object);

            //Act
            string result =_sut.ScanForOffense(new ScanInformation { Offense = ParkingOffense.BlockingFireHydrant, Tag = tag });

            //Assert
            Assert.AreEqual(magicString, result);
        }

    }
}
