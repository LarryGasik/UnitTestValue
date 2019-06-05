using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingTicket.DataAccess.DTO;

namespace ParkingTicket.DataAccess
{
    public interface IHolidayService
    {
        List<HolidayDTO> GetHolidays();
    }
}
