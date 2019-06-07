using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingTicketLogic.Generators
{
    public class TicketGenerator:ITicketGenerator
    {
        public string InstructionGenerator(bool TowCar, bool IssueTicket)
        {
            StringBuilder sb = new StringBuilder();
            if (TowCar)
            {
               sb.Append("Tow");
            }

            if (IssueTicket)
            {
                if (sb.Length != 0)
                {
                    sb.Append(Environment.NewLine);
                }
                sb.Append("here's your ticket");
            }

            return sb.ToString();
        }
    }
}
