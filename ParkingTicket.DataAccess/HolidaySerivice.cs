using System;
using System.Collections.Generic;
using ParkingTicket.DataAccess.DTO;

namespace ParkingTicket.DataAccess
{
    public class HolidaySerivice:IHolidayService
    {
        public List<HolidayDTO> GetHolidays()
        {
            List<HolidayDTO> Holidays = new List<HolidayDTO>();
            Holidays.Add(new HolidayDTO{Date= new DateTime(2019,01,01), TitleOfDay="New Years Day"});
            Holidays.Add(new HolidayDTO{Date= new DateTime(2019,07,04), TitleOfDay="Independence Day"});
            return Holidays;
        }
    }
}
