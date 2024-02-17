using NUnit.Framework;
using NUnit.Framework.Legacy;
using ParkingTicket.Logic.TowDeterminer.TowRules;

namespace ParkingTicket.Logic.Tests.TowDeterminer.TowRules;

[TestFixture]
public class TowIfVehicleHasThreeOrMoreTicketsTests
{
    private TowIfVehicleHasThreeOrMoreTickets _sut;

    [Test]
    public void DoNotTowCarsFor2Tickets()
    {
        _sut = new TowIfVehicleHasThreeOrMoreTickets(2);
        var result = _sut.ShouldTowCar();
        ClassicAssert.IsFalse(result);
    }

    [Test]
    public void TowCarsFor3Tickets()
    {
        _sut = new TowIfVehicleHasThreeOrMoreTickets(3);
        var result = _sut.ShouldTowCar();
        ClassicAssert.IsTrue(result);
    }

    [Test]
    public void TowCarsFor4Tickets()
    {
        _sut = new TowIfVehicleHasThreeOrMoreTickets(4);
        var result = _sut.ShouldTowCar();
        ClassicAssert.IsTrue(result);
    }
}