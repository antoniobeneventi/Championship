namespace Championship;

public class MatchWithResult : Match
{
    public int HomeTeamScore { get; }
    public int AwayTeamScore { get; }

    public MatchWithResult(TeamRecord homeTeam, TeamRecord awayTeam, DateTime matchDate, int homeTeamScore, int awayTeamScore, string stadiumName, string city)
        : base(homeTeam, awayTeam, matchDate, stadiumName, city)
    {
        HomeTeamScore = homeTeamScore;
        AwayTeamScore = awayTeamScore;
    }

    public override string ToString()
    {
        return $"{HomeTeam.SquadName} vs {AwayTeam.SquadName} - Date: {MatchDate.ToShortDateString()}, Score: {HomeTeamScore}-{AwayTeamScore}, Stadium: {StadiumName}, City: {City}";
    }
}
