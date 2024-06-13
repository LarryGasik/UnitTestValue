using ParkingTicket.DataAccess;

namespace ParkingTicket.DAL;

public class WeatherService : IWeatherService
{
    public bool IsSnowOnTheGroundByZip(int zipCode)
    {
        //This is just to simulate if it is snowing. It means nothing.
        return DateTime.Now.Second % 2 == 0;
        //Larry Messing With Turano
    }
}