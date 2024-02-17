namespace ParkingTicketLogic.Generators;

public interface ITicketGenerator
{
    string InstructionGenerator(bool TowCar, bool IssueTicket);
}