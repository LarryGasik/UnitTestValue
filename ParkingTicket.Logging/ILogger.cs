using System;

namespace ParkingTicket.Logging
{
    /// <summary>
    /// I am doing this to encapsulate any sort of logging.
    /// There's a fair bit of freedom here, but let's just
    /// assume that we're going to only have one logging
    /// class for these purposes. Maybe in the future
    /// you want to have different types of logging for
    /// troubleshooting, or email services.
    /// </summary>
    public interface ILogger
    {
        void LogException(Exception exception);
    }
}