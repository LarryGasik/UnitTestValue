namespace ParkingTicketLogic.Providers;

/// <summary>
///     Used for getting The Current Date
/// </summary>
public static class SystemTime
{
    /// <summary>
    ///     Normally this is a pass-through to DateTime.Now, but it can be overridden with SetDateTime( .. ) for testing or
    ///     debugging.
    /// </summary>
    public static Func<DateTime> Now = () => DateTime.Now;

    /// <summary>
    ///     Set time to return when SystemTime.Now() is called.
    /// </summary>
    public static void SetDateTime(DateTime dateTimeNow)
    {
        Now = () => dateTimeNow;
    }
}