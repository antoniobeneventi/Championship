﻿using System.Globalization;
using System.Resources;

namespace ChampionshipWebApp.Resources;

public static class Resources
{
    private static readonly ResourceManager ResourceManager =
        new ResourceManager("ChampionshipWebApp.Resources.index", typeof(Resources).Assembly);

    public static string WelcomeMessage => GetString("WelcomeMessage");
    public static string FootballLeague => GetString("FootballLeague");
    public static string AddTeams => GetString("AddTeams");
    public static string GenerateCalendar => GetString("GenerateCalendar");
    public static string ListOfTeams => GetString("ListOfTeams");
    public static string SquadName => GetString("SquadName");
    public static string FoundationYear => GetString("FoundationYear");
    public static string City => GetString("City");
    public static string ColorOfClub => GetString("ColorOfClub");
    public static string StadiumName => GetString("StadiumName");
    public static string Actions => GetString("Actions");
    public static string EditTeam => GetString("EditTeam");
    public static string DeleteButton => GetString("DeleteButton");
    public static string NoTeamsMessage => GetString("NoTeamsMessage");
    public static string CancelButton => GetString("CancelButton");
    public static string Logout => GetString("Logout");

    public static string DeleteChanges => GetString("DeleteChanges");
    public static string SaveChanges => GetString("SaveChanges");
    public static string ConfirmDelete => GetString("ConfirmDelete");
    public static string DeleteMessage => GetString("DeleteMessage");
    public static string FootballLeagueCalendar => GetString("FootballLeagueCalendar");
    public static string BackToHome => GetString("BackToHome");
    public static string ViewRankings => GetString("ViewRankings");
    public static string Matchday => GetString("Matchday");
    public static string HomeTeam => GetString("HomeTeam");
    public static string AwayTeam => GetString("AwayTeam");
    public static string Date => GetString("Date");
    public static string InsertEditResult => GetString("InsertEditResult");
    public static string Result => GetString("Result");
    public static string Submit => GetString("Submit");
    public static string EditResult => GetString("EditResult");
    public static string NotPlayed => GetString("NotPlayed");
    public static string BackToCalendar => GetString("BackToCalendar");
    public static string Rakings => GetString("Rakings");
    public static string GamesPlayed => GetString("GamesPlayed");
    public static string Wins => GetString("Wins");
    public static string Draws => GetString("Draws");
    public static string Defeats => GetString("Defeats");
    public static string GolScored => GetString("GolScored");
    public static string GolAgainst => GetString("GolAgainst");
    public static string Points => GetString("Points");
    public static string RelegationZone => GetString("RelegationZone");

    public static string FewTeams => GetString("FewTeams");
    public static string EditAccount => GetString("EditAccount");
    public static string ChangeLanguage => GetString("ChangeLanguage");
    public static string ChangePassword => GetString("ChangePassword");

    public static string SelectLanguage => GetString("SelectLanguage");
    public static string English => GetString("English");
    public static string Italian => GetString("Italian");






    private static string GetString(string name)
    {
        return ResourceManager.GetString(name, CultureInfo.CurrentUICulture);
    }
}