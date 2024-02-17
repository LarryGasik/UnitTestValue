using ParkingTicket.DAL;
using ParkingTicket.DataAccess;

namespace ParkingTicket.Logic.TowDeterminer.TowRules
{
    public class TowIfSnowOnGround : TowRule
    {
        private readonly IWeatherService _weatherService;
        private int _zipCode;

        //Todo: In the real world, we'd need some sort of flag
        //      where we account for the location of the car, and
        //      it being a tow zone if there's snow. Let's just
        //      assume right now we're towing all cars if there's
        //      snow in the zip code Maybe we could take
        //      advantage of GeoCoordinate?
        public TowIfSnowOnGround(int zipCode):this(new WeatherService(), zipCode)
        {
        }

        public TowIfSnowOnGround(IWeatherService weatherService, int zipCode)
        {
            _weatherService = weatherService;
            _zipCode = zipCode;
        }
        public override bool ShouldTowCar()
        {
            return _weatherService.IsSnowOnTheGroundByZip(_zipCode);
        }
    }
}
