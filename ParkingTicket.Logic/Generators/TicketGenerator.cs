using System.Text;

namespace ParkingTicketLogic.Generators;

public class TicketGenerator : ITicketGenerator
{
    public string InstructionGenerator(bool TowCar, bool IssueTicket)
    {
        var sb = new StringBuilder();
        if (TowCar) sb.Append("Tow");

        if (IssueTicket)
        {
            if (sb.Length != 0) sb.Append(Environment.NewLine);
            sb.Append("here's your ticket");
        }

        return sb.ToString();
    }
}