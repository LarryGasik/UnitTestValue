namespace ParkingTicket.Logic.TowDeterminer.TowRules;

public class TowIfVehicleHasThreeOrMoreTickets : TowRule
{
    private readonly int _numberOfTickets;

    public TowIfVehicleHasThreeOrMoreTickets(int numberOfTickets)
    {
        _numberOfTickets = numberOfTickets;
    }

    public override bool ShouldTowCar()
    {
        return _numberOfTickets >= 3;
    }
}