namespace Championship;

public class TeamStats
{
    public int TeamId { get; set; }
    public int Wins { get; set; }
    public int Draws { get; set; }
    public int Losses { get; set; }
    public int GoalsFor { get; set; }
    public int GoalsAgainst { get; set; }
    public int Points { get; set; }
    public int GamesPlayed { get; set; }

    public TeamStats()
    {
        Wins = 0;
        Draws = 0;
        Losses = 0;
        GoalsFor = 0;
        GoalsAgainst = 0;
        Points = 0;
        GamesPlayed = 0;
    }

    public int GoalDifference => GoalsFor - GoalsAgainst;


    public TeamStats(int wins, int draws, int losses, int goalsFor, int goalsAgainst, int points, int gamesPlayed)
    {
        // Controlla che il numero di vittorie, pareggi e sconfitte non siano negativi
        if (wins < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(wins), "Wins cannot be negative");
        }
        if (draws < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(draws), "Draws cannot be negative");
        }
        if (losses < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(losses), "Losses cannot be negative");
        }

        // Controlla che i gol segnati e subiti non siano negativi
        if (goalsFor < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(goalsFor), "GolFor cannot be negative.");
        }
        if (goalsAgainst < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(goalsAgainst), "GolAgainst cannot be negative");
        }

        // Controlla che la somma dei risultati non sia negativa
        if (wins + draws + losses < 0)
        {
            throw new ArgumentOutOfRangeException("The sum of wins, draws and losses cannot be negative.");
        }

        //controlla che i punti non siano negativi
        if (points < 0)
        {
            throw new ArgumentOutOfRangeException("The points cannot be negative");

        }
        //controlla che le partite giocate non siano negative
        if(GamesPlayed < 0)
        {
            throw new ArgumentOutOfRangeException("Game played cannot be negative");
        }

        Wins = wins;
        Draws = draws;
        Losses = losses;
        GoalsFor = goalsFor;
        GoalsAgainst = goalsAgainst;
        Points = (wins * 3) + (draws * 1);
        GamesPlayed = gamesPlayed;
    }

    public override string ToString()
    {
        return $"Games Played:{GamesPlayed}, Points: {Points},Wins: {Wins}, Draws: {Draws}, Losses: {Losses}, Goals For: {GoalsFor}, Goals Against: {GoalsAgainst}, Goal difference: {GoalsFor - GoalsAgainst}";
    }
}

