using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingTicket.DataAccess;

namespace ParkingTicketLogic.TowDeterminer.TowRuleEnforcements
{
    public class TowRuleEnforcementBase
    {
        private IWeatherService _iWeatherService;

        public TowRuleEnforcementBase() : this(new WeatherService())
        {

        }

        public TowRuleEnforcementBase(IWeatherService weatherService)
        {
            _iWeatherService = weatherService;
        }
    }
}
