using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ParkingTicket.DataAccess;
using ParkingTicket.DataAccess.DTO;
using ParkingTicket.Logging;
using ParkingTicketLogic.TowDeterminer;
using ParkingTicketLogic.TowDeterminer.TowRuleEnforcements;

namespace ParkingTicketLogic.Test.TowDeterminer
{
    [TestFixture]
    public class TowDeterminerServiceTests
    {
        private Mock<ILogger> _logger;
        private Mock<IStateParkingAuthority> _MY;
        private Mock<IStateParkingAuthority> _IL;
        private Mock<IStateParkingAuthority> _IN;
        private Mock<IStateParkingAuthority> _PA;
        private Mock<ITowRuleEnforcements> _EnforcementRules;
        private TowDeterminerService _sut;
        private string _tag;

        [SetUp]
        public void Setup()
        {
            //Todo: There's a way to mock the value of the string
            //      for different parameters, lengths, etc. Let's implement
            //      that later.
            _tag = Guid.NewGuid().ToString();
            _MY=new Mock<IStateParkingAuthority>();
            _IL = new Mock<IStateParkingAuthority>();
            _IN = new Mock<IStateParkingAuthority>();
            _PA = new Mock<IStateParkingAuthority>();
            _EnforcementRules = new Mock<ITowRuleEnforcements>();
            _logger = new Mock<ILogger>();
        }

        [Test]
        public void ShouldReturnValuesFromEnforcementRules()
        {
            //Arrange
            _MY.Setup(x => x.GetTicketsFromTag(_tag)).Returns(new List<ParkingTicketDto>());
            _IL.Setup(x => x.GetTicketsFromTag(_tag)).Returns(new List<ParkingTicketDto>());
            _IN.Setup(x => x.GetTicketsFromTag(_tag)).Returns(new List<ParkingTicketDto>());
            _PA.Setup(x => x.GetTicketsFromTag(_tag)).Returns(new List<ParkingTicketDto>());
            _EnforcementRules
                .Setup(x => x.ShouldTowCar(new List<ParkingTicketDto>(), ParkingOffense.BlockingSidewalk, It.IsAny<int>()))
                .Returns(true);
            _sut = new TowDeterminerService(_logger.Object,_MY.Object,_IL.Object, _IN.Object, _PA.Object, _EnforcementRules.Object);

            //Act
            bool result = _sut.ShouldTowCar(ParkingOffense.BlockingSidewalk, _tag, It.IsAny<int>());

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldCallAllParkingAuthorities()
        {
            //Arrange
            _sut = new TowDeterminerService(_logger.Object, _MY.Object, _IL.Object, _IN.Object, _PA.Object, _EnforcementRules.Object);

            //Act
            bool result = _sut.ShouldTowCar(ParkingOffense.BlockingSidewalk, _tag, It.IsAny<int>());

            //Assert
            _MY.Verify(x => x.GetTicketsFromTag(_tag), Times.Once);
            _IL.Verify(x => x.GetTicketsFromTag(_tag), Times.Once);
            _IN.Verify(x => x.GetTicketsFromTag(_tag), Times.Once);
            _PA.Verify(x => x.GetTicketsFromTag(_tag), Times.Once);
        }

        [Test]
        public void ShouldPassTicketsIntoEnforcementRules()
        {
            //Arrange
            ParkingTicketDto sampleTicket =  new ParkingTicketDto
            {
                DateOfOffense = DateTime.MinValue,Fine = 3, Offense = "bad hair",
                State="In", TicketID = Guid.NewGuid()
            };
            _MY.Setup(x => x.GetTicketsFromTag(_tag)).Returns(new List<ParkingTicketDto>());
            _IL.Setup(x => x.GetTicketsFromTag(_tag)).Returns(new List<ParkingTicketDto>());
            _IN.Setup(x => x.GetTicketsFromTag(_tag)).Returns(new List<ParkingTicketDto> {sampleTicket});
            _PA.Setup(x => x.GetTicketsFromTag(_tag)).Returns(new List<ParkingTicketDto>());
            _EnforcementRules
                .Setup(x => x.ShouldTowCar(new List<ParkingTicketDto>(), ParkingOffense.BlockingSidewalk, It.IsAny<int>()))
                .Returns(true);
            _sut = new TowDeterminerService(_logger.Object, _MY.Object, _IL.Object, _IN.Object, _PA.Object, _EnforcementRules.Object);

            //Act
            bool result = _sut.ShouldTowCar(ParkingOffense.BlockingSidewalk, _tag, It.IsAny<int>());

            //Assert
            _EnforcementRules.Verify(x=>x.ShouldTowCar(new List<ParkingTicketDto>{sampleTicket},  ParkingOffense.BlockingSidewalk, It.IsAny<int>()),Times.Once);
        }

        [Test]
        public void ShouldCallAllStateRepositoriesWhenMyStateExceptionIsThrown()
        {
            //Arrange
            _MY.Setup(x => x.GetTicketsFromTag(It.IsAny<string>())).Throws(new Exception("SomeExceptionThrown"));
            _sut = new TowDeterminerService(_logger.Object, _MY.Object, _IL.Object, _IN.Object, _PA.Object, _EnforcementRules.Object);

            //Act
            _sut.ShouldTowCar(ParkingOffense.BlockingSidewalk, _tag, It.IsAny<int>());

            //Assert
            _MY.Verify(x=>x.GetTicketsFromTag(_tag),Times.Once);
            _IL.Verify(x=>x.GetTicketsFromTag(_tag),Times.Once);
            _IN.Verify(x=>x.GetTicketsFromTag(_tag),Times.Once);
            _PA.Verify(x=>x.GetTicketsFromTag(_tag),Times.Once);
        }

        [Test]
        public void ShouldCallAllStateRepositoriesWhenPAExceptionIsThrown()
        {
            //Arrange
            _PA.Setup(x => x.GetTicketsFromTag(It.IsAny<string>())).Throws(new Exception("SomeExceptionThrown"));
            _sut = new TowDeterminerService(_logger.Object, _MY.Object, _IL.Object, _IN.Object, _PA.Object, _EnforcementRules.Object);

            //Act
            _sut.ShouldTowCar(ParkingOffense.BlockingSidewalk, _tag, It.IsAny<int>());

            //Assert
            _MY.Verify(x => x.GetTicketsFromTag(_tag), Times.Once);
            _IL.Verify(x => x.GetTicketsFromTag(_tag), Times.Once);
            _IN.Verify(x => x.GetTicketsFromTag(_tag), Times.Once);
            _PA.Verify(x => x.GetTicketsFromTag(_tag), Times.Once);
        }

        [Test]
        public void ShouldLogExceptionWhenPAExceptionIsThrown()
        {
            //Arrange
            SystemException someException = new SystemException("some Exception");
            _PA.Setup(x => x.GetTicketsFromTag(It.IsAny<string>())).Throws(someException);
            _sut = new TowDeterminerService(_logger.Object, _MY.Object, _IL.Object, _IN.Object, _PA.Object, _EnforcementRules.Object);

            //Act
            _sut.ShouldTowCar(ParkingOffense.BlockingSidewalk, _tag, It.IsAny<int>());

            //Assert
            _logger.Verify(x=>x.LogException(someException), Times.Once);
        }
    }
}