namespace ParkingTicketLogic.TowDeterminer.TowRules
{
    public class TowIfInHandicappedSpot:TowRule
    {
        private readonly ParkingOffense _offense;

        public TowIfInHandicappedSpot(ParkingOffense offense)
        {
            _offense = offense;
        }
        public override bool ShouldTowCar()
        {
            return _offense == ParkingOffense.HandicappedParkingSpot;
        }
    }
}
