

namespace Championship;

public class MatchWithResult
{
    public int HomeTeamScore { get; }
    public int AwayTeamScore { get; }
    public MatchWithResult(int homeTeamScore, int awayTeamScore)
    {
        HomeTeamScore = homeTeamScore;
        AwayTeamScore = awayTeamScore;
    }
    public override string ToString()
    {
        return $" Score: {HomeTeamScore}-{AwayTeamScore}";
    }
}
