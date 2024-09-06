using Championship;

public class LeagueStanding
{
    public Team Team { get; }
    public TeamStats Stats { get; }

    public LeagueStanding(Team team, TeamStats stats)
    {
        Team = team ?? throw new ArgumentNullException(nameof(team), "Team cannot be null");
        Stats = stats ?? throw new ArgumentNullException(nameof(stats), "Stats cannot be null");
    }

    public override string ToString()
    {
        return $"{Team.SquadName} - {Stats}";
    }

    // Metodo per generare la classifica per un intervallo di giornate
    public static List<LeagueStanding> GenerateStandings(Calendar calendar, int? from, int? to)
    {
        if (from.HasValue && to.HasValue && from > to)
        {
            throw new ArgumentException("From cannot be greater than to.");
        }

        
        var standingsDictionary = new Dictionary<string, TeamStats>();
        var teams = new HashSet<Team>();

        foreach (var matchday in calendar.Matchdays)
        {
            foreach (var match in matchday.Matches)
            {
                teams.Add(match.HomeTeam);
                teams.Add(match.AwayTeam);
            }
        }
        // Inizializza le statistiche delle squadre
        foreach (var team in teams)
        {
            standingsDictionary[team.SquadName] = new TeamStats(0, 0, 0, 0, 0, 0, 0);
        }

        // Determina l'intervallo di giornate
        int startMatchday = from ?? 1;
        int endMatchday = to ?? calendar.Matchdays.Count;

        if (startMatchday < 1 || endMatchday > calendar.Matchdays.Count)
        {
            throw new ArgumentOutOfRangeException("The matchday range is out of bounds.");
        }

        // Itera attraverso le giornate specificate e aggiorna le statistiche delle squadre
        foreach (var matchday in calendar.Matchdays)
        {
           
            foreach (var match in matchday.Matches)
            {
                if (match.Result != null)
                {
                    var homeTeamStats = standingsDictionary[match.HomeTeam.SquadName];
                    var awayTeamStats = standingsDictionary[match.AwayTeam.SquadName];

                    // Aggiorna statistiche della squadra di casa
                    if (match.Result.HomeTeamScore > match.Result.AwayTeamScore)
                    {
                        homeTeamStats = new TeamStats(
                            homeTeamStats.Wins + 1,
                            homeTeamStats.Draws,
                            homeTeamStats.Losses,
                            homeTeamStats.GoalsFor + match.Result.HomeTeamScore,
                            homeTeamStats.GoalsAgainst + match.Result.AwayTeamScore,
                            homeTeamStats.Points + 3,
                            homeTeamStats.GamesPlayed + 1
                        );

                        awayTeamStats = new TeamStats(
                            awayTeamStats.Wins,
                            awayTeamStats.Draws,
                            awayTeamStats.Losses + 1,
                            awayTeamStats.GoalsFor + match.Result.AwayTeamScore,
                            awayTeamStats.GoalsAgainst + match.Result.HomeTeamScore,
                            awayTeamStats.Points,
                            awayTeamStats.GamesPlayed + 1
                        );
                    }
                    else if (match.Result.HomeTeamScore < match.Result.AwayTeamScore)
                    {
                        awayTeamStats = new TeamStats(
                            awayTeamStats.Wins + 1,
                            awayTeamStats.Draws,
                            awayTeamStats.Losses,
                            awayTeamStats.GoalsFor + match.Result.AwayTeamScore,
                            awayTeamStats.GoalsAgainst + match.Result.HomeTeamScore,
                            awayTeamStats.Points + 3,
                            awayTeamStats.GamesPlayed + 1
                        );

                        homeTeamStats = new TeamStats(
                            homeTeamStats.Wins,
                            homeTeamStats.Draws,
                            homeTeamStats.Losses + 1,
                            homeTeamStats.GoalsFor + match.Result.HomeTeamScore,
                            homeTeamStats.GoalsAgainst + match.Result.AwayTeamScore,
                            homeTeamStats.Points,
                            homeTeamStats.GamesPlayed + 1
                        );
                    }
                    else
                    {
                        homeTeamStats = new TeamStats(
                            homeTeamStats.Wins,
                            homeTeamStats.Draws + 1,
                            homeTeamStats.Losses,
                            homeTeamStats.GoalsFor + match.Result.HomeTeamScore,
                            homeTeamStats.GoalsAgainst + match.Result.AwayTeamScore,
                            homeTeamStats.Points + 1,
                            homeTeamStats.GamesPlayed + 1
                        );

                        awayTeamStats = new TeamStats(
                            awayTeamStats.Wins,
                            awayTeamStats.Draws + 1,
                            awayTeamStats.Losses,
                            awayTeamStats.GoalsFor + match.Result.AwayTeamScore,
                            awayTeamStats.GoalsAgainst + match.Result.HomeTeamScore,
                            awayTeamStats.Points + 1,
                            awayTeamStats.GamesPlayed + 1
                        );
                    }

                    standingsDictionary[match.HomeTeam.SquadName] = homeTeamStats;
                    standingsDictionary[match.AwayTeam.SquadName] = awayTeamStats;
                }
            }
        }

        // Crea e ordina l'elenco finale di LeagueStanding
        var standings = standingsDictionary
            .Select(kvp => new LeagueStanding(
                teams.First(t => t.SquadName == kvp.Key),
                kvp.Value
            ))
            .OrderByDescending(ts => ts.Stats.Points)
            .ThenByDescending(ts => ts.Stats.GoalsFor - ts.Stats.GoalsAgainst)
            .ThenByDescending(ts => ts.Stats.Wins)
            .ThenByDescending(ts => ts.Stats.Draws)
            .ThenByDescending(ts => ts.Stats.Losses)
            .ThenByDescending(ts => ts.Stats.GoalsFor)
            .ThenByDescending(ts => ts.Stats.GoalsAgainst)
            .ToList();

        return standings;
    }
}


