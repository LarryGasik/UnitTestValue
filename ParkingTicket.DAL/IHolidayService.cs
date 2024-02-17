using ParkingTicket.DataAccess.DTO;

namespace ParkingTicket.DataAccess;

public interface IHolidayService
{
    List<HolidayDTO> GetHolidays();
}