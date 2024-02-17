using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using ParkingTicket.DAL;
using ParkingTicket.DataAccess;
using ParkingTicket.DataAccess.DTO;
using ParkingTicket.Logic;
using ParkingTicketLogic.Providers;

namespace ParkingTicketLogic.Test;

[TestFixture]
public class TicketIssuerTests
{
    [SetUp]
    public void SetUp()
    {
        _holidayService = new Mock<IHolidayService>();
        _myStateParkingAuthority = new Mock<IMyStateParkingAuthority>();
        _tag = Guid.NewGuid().ToString();
    }

    private TicketIssuer _sut;
    private Mock<IHolidayService> _holidayService;
    private Mock<IMyStateParkingAuthority> _myStateParkingAuthority;
    private string _tag;

    [Test]
    public void ShouldReturnTrueHydrantOnNonHoliday()
    {
        //Arrange
        SystemTime.SetDateTime(new DateTime(2019, 05, 22));
        _holidayService.Setup(x => x.GetHolidays())
            .Returns(
                new List<HolidayDTO>
                {
                    new() { Date = new DateTime(2019, 05, 22), TitleOfDay = "Mayor's Birthday" }
                });
        _myStateParkingAuthority.Setup(x => x.GetTicketsFromTag(_tag)).Returns(new List<ParkingTicketDto>());
        _sut = new TicketIssuer(_holidayService.Object, _myStateParkingAuthority.Object);

        //Act
        var result = _sut.DetermineTicket(ParkingOffense.BlockingFireHydrant, _tag);

        //Assert
        ClassicAssert.IsTrue(result);
    }

    [Test]
    public void ShouldIssueTicketForHydrant()
    {
        //Arrange
        SystemTime.SetDateTime(new DateTime(2019, 05, 22));
        _holidayService.Setup(x => x.GetHolidays())
            .Returns(
                new List<HolidayDTO>
                {
                    new() { Date = new DateTime(2019, 05, 22), TitleOfDay = "Mayor's Birthday" }
                });
        _myStateParkingAuthority.Setup(x => x.GetTicketsFromTag(_tag)).Returns(new List<ParkingTicketDto>());
        _sut = new TicketIssuer(_holidayService.Object, _myStateParkingAuthority.Object);

        //Act
        var result = _sut.DetermineTicket(ParkingOffense.BlockingFireHydrant, _tag);

        //Assert
        _myStateParkingAuthority.Verify(x => x.IssueParkingTicketDto(ParkingOffense.BlockingFireHydrant.ToString(), 30),
            Times.Once);
    }

    [Test]
    public void ShouldReturnTrueExpiredMeterOnNonHoliday()
    {
        //Arrange
        SystemTime.SetDateTime(new DateTime(2019, 05, 24));
        _holidayService.Setup(x => x.GetHolidays())
            .Returns(
                new List<HolidayDTO>
                {
                    new() { Date = new DateTime(2019, 05, 22), TitleOfDay = "Mayor's Birthday" }
                });
        _myStateParkingAuthority.Setup(x => x.GetTicketsFromTag(_tag)).Returns(new List<ParkingTicketDto>());
        _sut = new TicketIssuer(_holidayService.Object, _myStateParkingAuthority.Object);

        //Act
        var result = _sut.DetermineTicket(ParkingOffense.ExpiredParkingMeter, _tag);

        //Assert
        ClassicAssert.IsTrue(result);
    }

    [Test]
    public void ShouldIssueTicketForExpiredMeterOnNonHoliday()
    {
        //Arrange
        SystemTime.SetDateTime(new DateTime(2019, 05, 24));
        _holidayService.Setup(x => x.GetHolidays())
            .Returns(
                new List<HolidayDTO>
                {
                    new() { Date = new DateTime(2019, 05, 22), TitleOfDay = "Mayor's Birthday" }
                });
        _myStateParkingAuthority.Setup(x => x.GetTicketsFromTag(_tag)).Returns(new List<ParkingTicketDto>());
        _sut = new TicketIssuer(_holidayService.Object, _myStateParkingAuthority.Object);

        //Act
        var result = _sut.DetermineTicket(ParkingOffense.ExpiredParkingMeter, _tag);

        //Assert
        _myStateParkingAuthority.Verify(x => x.IssueParkingTicketDto(ParkingOffense.ExpiredParkingMeter.ToString(), 30),
            Times.Once);
    }

    [Test]
    public void ShouldReturnFalseExpiredMeterOnNonHoliday()
    {
        //Arrange
        SystemTime.SetDateTime(new DateTime(2019, 05, 22));
        _holidayService.Setup(x => x.GetHolidays())
            .Returns(
                new List<HolidayDTO>
                {
                    new() { Date = new DateTime(2019, 05, 22), TitleOfDay = "Mayor's Birthday" }
                });
        _myStateParkingAuthority.Setup(x => x.GetTicketsFromTag(_tag)).Returns(new List<ParkingTicketDto>());
        _sut = new TicketIssuer(_holidayService.Object, _myStateParkingAuthority.Object);

        //Act
        var result = _sut.DetermineTicket(ParkingOffense.ExpiredParkingMeter, _tag);

        //Assert
        ClassicAssert.IsFalse(result);
    }

    [Test]
    public void ShouldNotIssueTicketForExpiredMeterOnHoliday()
    {
        //Arrange
        SystemTime.SetDateTime(new DateTime(2019, 05, 22));
        _holidayService.Setup(x => x.GetHolidays())
            .Returns(
                new List<HolidayDTO>
                {
                    new() { Date = new DateTime(2019, 05, 22), TitleOfDay = "Mayor's Birthday" }
                });
        _myStateParkingAuthority.Setup(x => x.GetTicketsFromTag(_tag)).Returns(new List<ParkingTicketDto>());
        _sut = new TicketIssuer(_holidayService.Object, _myStateParkingAuthority.Object);

        //Act
        var result = _sut.DetermineTicket(ParkingOffense.ExpiredParkingMeter, _tag);

        //Assert
        _myStateParkingAuthority.Verify(x => x.IssueParkingTicketDto(It.IsAny<string>(), It.IsAny<int>()), Times.Never);
    }

    [Test]
    public void ShouldNotIssueTicketForSameViolationOnSameDay()
    {
        //Arrange
        SystemTime.SetDateTime(new DateTime(2019, 05, 22));
        _holidayService.Setup(x => x.GetHolidays())
            .Returns(new List<HolidayDTO>());

        var exisitingTicket = new ParkingTicketDto
        {
            DateOfOffense = SystemTime.Now(), Fine = 43, Offense = ParkingOffense.ExpiredParkingMeter.ToString(),
            State = string.Empty
        };
        _myStateParkingAuthority.Setup(x => x.GetTicketsFromTag(_tag))
            .Returns(new List<ParkingTicketDto> { exisitingTicket });
        _sut = new TicketIssuer(_holidayService.Object, _myStateParkingAuthority.Object);

        //Act
        var result = _sut.DetermineTicket(ParkingOffense.ExpiredParkingMeter, _tag);

        //Assert
        _myStateParkingAuthority.Verify(x => x.IssueParkingTicketDto(It.IsAny<string>(), It.IsAny<int>()), Times.Never);
    }

    [Test]
    public void ShouldReturnFalseForSameViolationOnSameDay()
    {
        //Arrange
        SystemTime.SetDateTime(new DateTime(2019, 05, 22));
        _holidayService.Setup(x => x.GetHolidays())
            .Returns(new List<HolidayDTO>());

        var exisitingTicket = new ParkingTicketDto
        {
            DateOfOffense = SystemTime.Now(),
            Fine = 43,
            Offense = ParkingOffense.ExpiredParkingMeter.ToString(),
            State = string.Empty
        };
        _myStateParkingAuthority.Setup(x => x.GetTicketsFromTag(_tag))
            .Returns(new List<ParkingTicketDto> { exisitingTicket });
        _sut = new TicketIssuer(_holidayService.Object, _myStateParkingAuthority.Object);

        //Act
        var result = _sut.DetermineTicket(ParkingOffense.ExpiredParkingMeter, _tag);

        //Assert
        ClassicAssert.IsFalse(result);
    }

    [Test]
    public void ShouldNotCareAboutHolidaysWhenParkingInAHandicappedSpot()
    {
        //Scenario: Users report slowness when issuing tickets.
        //          One optimization we can make is only call the 
        //          holiday lookups when it is a holiday.


        //Arrange
        _myStateParkingAuthority.Setup(x => x.GetTicketsFromTag(_tag)).Returns(new List<ParkingTicketDto>());
        _sut = new TicketIssuer(_holidayService.Object, _myStateParkingAuthority.Object);

        //Act
        var result = _sut.DetermineTicket(ParkingOffense.HandicappedParkingSpot, _tag);

        //Assert
        _holidayService.Verify(x => x.GetHolidays(), Times.Never);
    }
}