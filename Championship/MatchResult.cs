namespace Championship;

public class MatchResult
{
    public int HomeTeamScore { get; }
    public int AwayTeamScore { get; }
    public MatchResult(int homeTeamScore, int awayTeamScore)
    {
        if (homeTeamScore < 0) //controlla se il risultato della squadra di casa non sia negativo
        {
            throw new ArgumentOutOfRangeException(nameof(homeTeamScore), "Home team score cannot be negative.");
        }

        if (awayTeamScore < 0)//controlla se il risultato della squadra ospite non sia negativo
        {
            throw new ArgumentOutOfRangeException(nameof(awayTeamScore), "Away team score cannot be negative.");
        }

        HomeTeamScore = homeTeamScore;
        AwayTeamScore = awayTeamScore;
    }
    public override string ToString()
    {
        return $" Score: {HomeTeamScore}-{AwayTeamScore}";
    }
}
