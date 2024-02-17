namespace ParkingTicketLogic.DTO;

public class ScanInformation
{
    public string Tag { get; set; }
    public ParkingOffense Offense { get; set; }
    public int zipCode { get; set; }
}