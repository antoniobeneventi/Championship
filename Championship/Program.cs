namespace Championship;

class Program
{
    static void Main(string[] args)
    {

        League league = new League();
        var predefinedTeams = new List<Team>
        {
            new Team("Juventus", 1897, "Turin", "Black and White", "Allianz Stadium"),
            new Team("Milan", 1899, "Milan", "Red and Black", "San Siro"),
            new Team("Roma", 1927, "Rome", "Red and Yellow", "Stadio Olimpico"),
            new Team("Napoli", 1926, "Naples", "Blue and White", "Stadio Diego Armando Maradona"),
        };

        foreach (var team in predefinedTeams)
        {
            league.AddTeam(team);
        }
        Console.WriteLine(league.ToString());



        CalendarGenerator calendarGenerator = new CalendarGenerator();
        Calendar calendarWithoutResults = calendarGenerator.GenerateCalendar(predefinedTeams);
        // Visualizza il calendario senza risultati
        Console.WriteLine("\nCalendar without results:");
        Console.WriteLine(calendarWithoutResults);

        // Imposta i risultati per tutte le partite nel calendario
        var predefinedResults = new List<(string HomeTeam, string AwayTeam, MatchResult Result)>
        {
            ("Juventus", "Napoli", new MatchResult(2, 1)),
            ("Milan", "Roma", new MatchResult(1, 1)),
            ( "Roma","Juventus" ,new MatchResult(3, 0)),
            ("Milan", "Napoli", new MatchResult(0, 2)),
            ("Juventus", "Milan", new MatchResult(1, 2)),
            ("Roma","Napoli", new MatchResult(2, 2)),
            //ritorno
            ("Napoli","Juventus", new MatchResult(1, 1)),
            ("Roma", "Milan", new MatchResult(0, 1)),
            ("Juventus","Roma", new MatchResult(2, 1)),
            ("Napoli", "Milan", new MatchResult(1, 0)),
            ("Milan", "Juventus", new MatchResult(0, 0)),
            ("Napoli", "Roma", new MatchResult(2, 1))
        };

        int resultIndex = 0;

        foreach (var matchday in calendarWithoutResults.Matchdays)
        {
            foreach (var match in matchday.Matches)
            {
                var result = predefinedResults[resultIndex];
                if (match.HomeTeam.SquadName == result.HomeTeam && match.AwayTeam.SquadName == result.AwayTeam)
                {
                    match.SetResult(result.Result);
                    resultIndex++;
                }
            }
        }
        // Visualizza il calendario con i risultati
        Console.WriteLine("\nCalendar with results:");
        Console.WriteLine(calendarWithoutResults);

        //Console.WriteLine("\n insert the number of matchday for view the standings");
        //int matchdayInput = int.Parse(Console.ReadLine());

        LeagueStandingsGenerator standingsGenerator = new LeagueStandingsGenerator();
        LeagueStandings finalStandings = standingsGenerator.GenerateStandings(calendarWithoutResults, predefinedTeams /*matchdayInput*/);

        // Visualizza la classifica
        foreach (var standing in finalStandings.Standings)
        {
            Console.WriteLine(standing);
        }
    }
}


