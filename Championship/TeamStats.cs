namespace Championship;

public class TeamStats
{
    public int Wins { get; }
    public int Draws { get; }
    public int Losses { get; }
    public int GoalsFor { get; }
    public int GoalsAgainst { get; }


    public TeamStats(int wins, int draws, int losses, int goalsFor, int goalsAgainst)
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

        Wins = wins;
        Draws = draws;
        Losses = losses;
        GoalsFor = goalsFor;
        GoalsAgainst = goalsAgainst;
    }

    public override string ToString()
    {
        return $"Stats - Wins: {Wins}, Draws: {Draws}, Losses: {Losses}, Goals For: {GoalsFor}, Goals Against: {GoalsAgainst}";
    }
}

