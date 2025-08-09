using Microsoft.Extensions.DependencyInjection;
using ParkingTicket.DAL;
using ParkingTicket.DataAccess;
using ParkingTicket.DataAccess.StateParkingAuthorities;
using ParkingTicket.Logic;
using ParkingTicket.Logic.TowDeterminer;
using ParkingTicket.Logic.TowDeterminer.TowRuleEnforcements;
using ParkingTicket.Logging;
using ParkingTicketLogic;
using ParkingTicketLogic.DTO;
using ParkingTicketLogic.Generators;
using ParkingTicketLogic.TowDeterminer.TowRuleEnforcements;

namespace ParkingTicketUI;

internal class Program
{
    //changes on masters
    private static void Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddSingleton<ILogger, Logger>();
        services.AddSingleton<IHolidayService, HolidaySerivice>();
        services.AddSingleton<IMyStateParkingAuthority, MyStateParkingAuthority>();
        services.AddSingleton<IStateParkingAuthority, MyStateParkingAuthority>();
        services.AddSingleton<IStateParkingAuthority, IllinoisParkingAuthority>();
        services.AddSingleton<IStateParkingAuthority, IndianaParingAuthority>();
        services.AddSingleton<IStateParkingAuthority, PennsylvaniaParkingAuthority>();
        services.AddSingleton<ITicketIssuer, TicketIssuer>();
        services.AddSingleton<ITowRuleEnforcements, TowRuleEnforcementsSpring2019>();
        services.AddSingleton<ITowDeterminerService, TowDeterminerService>();
        services.AddSingleton<ITicketGenerator, TicketGenerator>();
        services.AddSingleton<ParkingTicketCalculator>();

        using var serviceProvider = services.BuildServiceProvider();
        var ptc = serviceProvider.GetRequiredService<ParkingTicketCalculator>();

        var myOffense = ParkingOffense.UnknownParkingOffense;
        //Todo: We have to validate user Input. We could pass ham as an argument
        //      and it shouldn't throw an exception. Maybe consider moving 
        //      the parsing to business logic so that we don't have to do this
        //      for the front end.
        if (Enum.IsDefined(typeof(ParkingOffense), Convert.ToInt32(args[1])))
            myOffense = (ParkingOffense)Convert.ToInt32(args[1]);


        var tag = args[0];

        //Todo: Probably should have verification on this so that we fail safely
        //      when someone enters ham as a zip.
        var zip = Convert.ToInt32(args[2]);
        Console.WriteLine("Parking Offense: " + myOffense);
        Console.WriteLine("Tag:             " + tag);
        Console.WriteLine("Zip Code:        " + zip);
        Console.WriteLine("Scan Started at: " + DateTime.Now);

        var result = ptc.ScanForOffense(new ScanInformation { Offense = myOffense, Tag = tag, zipCode = zip });
        Console.WriteLine(Environment.NewLine);
        Console.WriteLine(result);
        Console.WriteLine(Environment.NewLine);
        Console.WriteLine("Scan Done at:    " + DateTime.Now);
        Console.WriteLine("Press any key to close.");
        Console.ReadKey();
    }
}
