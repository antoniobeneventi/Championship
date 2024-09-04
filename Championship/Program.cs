namespace Championship;

class Program
{
    static void Main(string[] args)
    {
        League league = new League();
        var predefinedTeams = new List<TeamRecord>
        {
            new TeamRecord("Juventus", 1897, "Turin", "Black and White", "Allianz Stadium"),
            new TeamRecord("Milan", 1899, "Milan", "Red and Black", "San Siro"),
            new TeamRecord("Inter", 1908, "Milan", "Black and Blue", "San Siro"),
            new TeamRecord("Roma", 1927, "Rome", "Red and Yellow", "Stadio Olimpico"),
            new TeamRecord("Napoli", 1926, "Naples", "Blue and White", "Stadio Diego Armando Maradona"),
            new TeamRecord("Lazio", 1900, "Rome", "Blue and White", "Stadio Olimpico"),
            new TeamRecord("Atalanta", 1907, "Bergamo", "Blue and Black", "Gewiss Stadium"),
            new TeamRecord("Fiorentina", 1926, "Florence", "Purple", "Stadio Artemio Franchi"),
            new TeamRecord("Bologna", 1909, "Bologna", "Red and Blue", "Stadio Renato Dall'Ara"),
            new TeamRecord("Torino", 1906, "Turin", "Garnet", "Stadio Olimpico Grande Torino")
        };

        foreach (var team in predefinedTeams)
        {
            league.AddTeam(team);
        }
        league.ShowTeams();

        // calendar without results
        CalendarGenerator calendarGenerator = new CalendarGenerator();
        Calendar calendarWithoutResults = calendarGenerator.GenerateCalendar(predefinedTeams);
        Console.WriteLine("\nCalendar without Results:");
        calendarWithoutResults.ShowCalendar();

        // calendar with results
        GenerateCalendarWithResult calendarWithResultGenerator = new GenerateCalendarWithResult();
        Calendar calendarWithResults = calendarWithResultGenerator.GenerateCalendarWithScores(predefinedTeams);
        Console.WriteLine("\nCalendar with Results:");
        calendarWithResults.ShowCalendar();

    }
    
}

