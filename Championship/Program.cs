namespace Championship;

class Program
{
    static void Main(string[] args)
    {
        // Crea una nuova lega e aggiungi 4 squadre
        League league = new League();
        var predefinedTeams = new List<TeamRecord>
        {
            new TeamRecord("Juventus", 1897, "Turin", "Black and White", "Allianz Stadium"),
            new TeamRecord("Milan", 1899, "Milan", "Red and Black", "San Siro"),
            new TeamRecord("Roma", 1927, "Rome", "Red and Yellow", "Stadio Olimpico"),
            new TeamRecord("Napoli", 1926, "Naples", "Blue and White", "Stadio Diego Armando Maradona"),
        };

        foreach (var team in predefinedTeams)
        {
            league.AddTeam(team);
        }
        league.ShowTeams();

        // Crea un generatore di calendario e genera il calendario senza risultati
        CalendarGenerator calendarGenerator = new CalendarGenerator();
        Calendar calendarWithoutResults = calendarGenerator.GenerateCalendar(predefinedTeams);
        Console.WriteLine("\nCalendar without results:");
        calendarWithoutResults.ShowCalendar();

        // Imposta i risultati per tutte le partite nel calendario
        var predefinedResults = new List<(string HomeTeam, string AwayTeam, MatchResult Result)>
        {
            ("Juventus", "Napoli", new MatchResult(2, 1)),
            ("Milan", "Roma", new MatchResult(1, 1)),
            ( "Roma","Juventus" ,new MatchResult(3, 0)),
            ("Milan", "Napoli", new MatchResult(0, 2)),
            ("Juventus", "Milan", new MatchResult(1, 2)),
            ("Roma","Napoli", new MatchResult(2, 2)),
            ("Napoli","Juventus", new MatchResult(1, 1)), // Ritorno
            ("Roma", "Milan", new MatchResult(0, 1)), // Ritorno
            ("Juventus","Roma", new MatchResult(2, 1)), // Ritorno
            ("Napoli", "Milan", new MatchResult(1, 0)), // Ritorno
            ("Milan", "Juventus", new MatchResult(0, 0)), // Ritorno
            ("Napoli", "Roma", new MatchResult(2, 1))  // Ritorno
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
        calendarWithoutResults.ShowCalendar();
    }
}


