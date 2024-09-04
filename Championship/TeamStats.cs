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

