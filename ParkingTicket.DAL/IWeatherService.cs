namespace ParkingTicket.DataAccess;

public interface IWeatherService
{
    bool IsSnowOnTheGroundByZip(int zipCode);
}