namespace Championship;

class Program
{
    static void Main(string[] args)
    {
        string databasePath = "football_league.db"; // Specifica il percorso del database
        DatabaseManager dbManager = new DatabaseManager(databasePath);

        
        dbManager.CreateTables();

        dbManager.InsertData();


        // Crea una nuova lega e aggiunge 4 squadre
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

        // Crea un generatore di calendario e genera il calendario senza risultati
        CalendarGenerator calendarGenerator = new CalendarGenerator();
        Calendar calendarWithoutResults = calendarGenerator.GenerateCalendar(predefinedTeams);
        Console.WriteLine("\nCalendar without results:");
        Console.WriteLine(calendarWithoutResults);

        // Imposta i risultati per tutte le partite nel calendario
        var predefinedResults = new List<(string HomeTeam, string AwayTeam, MatchResult Result)>
        {
            ("Juventus", "Napoli", new MatchResult(2, 1)),
            ("Milan", "Roma", new MatchResult(1, 1)),
            ("Roma", "Juventus", new MatchResult(3, 0)),
            ("Milan", "Napoli", new MatchResult(0, 2)),
            ("Juventus", "Milan", new MatchResult(1, 2)),
            ("Roma", "Napoli", new MatchResult(2, 2)),
            //ritorno
            ("Napoli", "Juventus", new MatchResult(1, 1)),
            ("Roma", "Milan", new MatchResult(0, 1)),
            ("Juventus", "Roma", new MatchResult(2, 1)),
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


        // Genera la classifica nel range richiesto
        var standingsFromTo = LeagueStanding.GenerateStandings(calendarWithoutResults, null, null);



        // Visualizza le classifiche
        Console.WriteLine("\nRanking in the required range:");
        foreach (var standing in standingsFromTo)
        {
            Console.WriteLine(standing);
        }

        var standings = dbManager.GetStandingsForMatchdayRange(1, 6);
        Console.WriteLine("\nRanking in the required range:");
        foreach (var standing in standings)
        {
            Console.WriteLine($"\nTeam: {standing.SquadName}, Points: {standing.Points}, Wins: {standing.Wins}, Draws: {standing.Draws}, Losses: {standing.Losses}, Goal Difference: {standing.GoalDifference}");
        }

    }
}


