using System.Globalization;
using System.Resources;

namespace ChampionshipWebApp.Resources;

public static class Resources
{
    private static readonly ResourceManager ResourceManager =
        new ResourceManager("ChampionshipWebApp.Resources.index", typeof(Resources).Assembly);

    public static string WelcomeMessage => GetString("WelcomeMessage");
    public static string FootballLeague => GetString("FootballLeague");
    public static string AddTeams => GetString("AddTeams");
    //public static string GenerateCalendar => GetString("GenerateCalendar");
    public static string ListOfTeams => GetString("ListOfTeams");
    //public static string SquadName => GetString("SquadName");
    //public static string FoundationYear => GetString("FoundationYear");
    //public static string City => GetString("City");
    //public static string ColorOfClub => GetString("ColorOfClub");
    //public static string StadiumName => GetString("StadiumName");
    //public static string Actions => GetString("Actions");
    public static string EditTeam => GetString("EditTeam");
    public static string ConfirmDeletion => GetString("ConfirmDeletion");
    public static string NoTeamsMessage => GetString("NoTeamsMessage");

    private static string GetString(string name)
    {
        return ResourceManager.GetString(name, CultureInfo.CurrentUICulture);
    }
}