using ParkingTicket.DataAccess.DTO;

namespace ParkingTicket.DataAccess;

public interface IHolidayService
{
    // turano-feature-2 change
    List<HolidayDTO> GetHolidays();
}