using NUnit.Framework;
using NUnit.Framework.Legacy;
using ParkingTicket.Logic.TowDeterminer.TowRules;

namespace ParkingTicket.Logic.Tests.TowDeterminer.TowRules;

[TestFixture]
public class TowIfTotalFinesEquateMoreThanMaximumAmountTests
{
    [SetUp]
    public void Setup()
    {
    }

    private TowIfTotalFinesEquateMoreThanMaximumAmount _sut;

    [Test]
    public void DoNotTowCarsIfTotalFineEqualToMax()
    {
        _sut = new TowIfTotalFinesEquateMoreThanMaximumAmount(300);
        var result = _sut.ShouldTowCar();
        ClassicAssert.IsFalse(result);
    }

    [Test]
    public void TowCarsIfGreaterThanMax()
    {
        _sut = new TowIfTotalFinesEquateMoreThanMaximumAmount(301);
        var result = _sut.ShouldTowCar();
        ClassicAssert.IsTrue(result);
    }

    [Test]
    public void TowCarsIfLessThanMax()
    {
        _sut = new TowIfTotalFinesEquateMoreThanMaximumAmount(299);
        var result = _sut.ShouldTowCar();
        ClassicAssert.IsFalse(result);
    }
}