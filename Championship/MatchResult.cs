using System.ComponentModel.DataAnnotations;

namespace Championship;

public class MatchResult
{
    [Key]
    public int Id { get; set; }

    public int HomeTeamScore { get; set; } 
    public int AwayTeamScore { get; set; } 

    public MatchResult()
    {
    }

    public MatchResult(int homeTeamScore, int awayTeamScore)
    {
        if (homeTeamScore < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(homeTeamScore), "Home team score cannot be negative.");
        }

        if (awayTeamScore < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(awayTeamScore), "Away team score cannot be negative.");
        }

        HomeTeamScore = homeTeamScore;
        AwayTeamScore = awayTeamScore;
    }

    public override string ToString()
    {
        return $"Score: {HomeTeamScore}-{AwayTeamScore}";
    }
}
