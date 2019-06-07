namespace ParkingTicketLogic.Generators
{
    interface ITicketGenerator
    {
        string InstructionGenerator(bool TowCar, bool IssueTicket);
    }
}
