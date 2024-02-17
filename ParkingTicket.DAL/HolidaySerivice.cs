using ParkingTicket.DataAccess;
using ParkingTicket.DataAccess.DTO;

namespace ParkingTicket.DAL;

public class HolidaySerivice : IHolidayService
{
    public List<HolidayDTO> GetHolidays()
    {
        var Holidays = new List<HolidayDTO>();
        Holidays.Add(new HolidayDTO { Date = new DateTime(2019, 01, 01), TitleOfDay = "New Years Day" });
        Holidays.Add(new HolidayDTO { Date = new DateTime(2019, 07, 04), TitleOfDay = "Independence Day" });
        return Holidays;
    }
}