namespace ParkingTicket.Logic.TowDeterminer.TowRules;

public class TowIfTotalFinesEquateMoreThanMaximumAmount : TowRule
{
    private readonly int _finesOwed;

    public TowIfTotalFinesEquateMoreThanMaximumAmount(int finesOwed)
    {
        _finesOwed = finesOwed;
    }

    public override bool ShouldTowCar()
    {
        return _finesOwed > 300;
    }
}