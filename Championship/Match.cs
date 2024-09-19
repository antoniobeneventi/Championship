
using System.ComponentModel.DataAnnotations.Schema;

namespace Championship;

public class Match
{
    public int Id { get; set; }
    public int HomeTeamId { get; set; }
    public int AwayTeamId { get; set; }
    public int? ResultId { get; set; } 

    public DateTime MatchDate { get; set; }
    public string StadiumName { get; set; }
    public string City { get; set; }
    public MatchResult? Result { get; set; }

    public Team HomeTeam { get; set; }
    public Team AwayTeam { get; set; }

    [NotMapped]
    public string HomeTeamName => HomeTeam?.SquadName;

    [NotMapped]
    public string AwayTeamName => AwayTeam?.SquadName;

    public Match() { }

    public Match(Team homeTeam, Team awayTeam, DateTime matchDate, string stadiumName, string city)
    {
        if (homeTeam == null) throw new ArgumentNullException(nameof(homeTeam));
        if (awayTeam == null) throw new ArgumentNullException(nameof(awayTeam));
        if (string.IsNullOrWhiteSpace(stadiumName)) throw new ArgumentException("Stadium name cannot be null or empty.", nameof(stadiumName));
        if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException("City cannot be null or empty.", nameof(city));

        HomeTeam = homeTeam;
        AwayTeam = awayTeam;
        MatchDate = matchDate;
        StadiumName = stadiumName;
        City = city;
        Result = null;
    }

    public void SetResult(MatchResult result)
    {
        Result = result ?? throw new ArgumentNullException(nameof(result));
        ResultId = result.Id; 
    }

    public override bool Equals(object obj)
    {
        if (obj is Match otherMatch)
        {
            bool sameTeams = (HomeTeam.Equals(otherMatch.HomeTeam) && AwayTeam.Equals(otherMatch.AwayTeam)) ||
                             (HomeTeam.Equals(otherMatch.AwayTeam) && AwayTeam.Equals(otherMatch.HomeTeam));
            return sameTeams;
        }
        return false;
    }

    public override int GetHashCode()
    {
        int homeTeamHash = HomeTeam?.GetHashCode() ?? 0;
        int awayTeamHash = AwayTeam?.GetHashCode() ?? 0;
        return homeTeamHash + awayTeamHash;
    }

    public override string ToString()
    {
        string resultString = Result != null ? $"{Result.HomeTeamScore} - {Result.AwayTeamScore}" : "vs";
        return $"{HomeTeamName ?? "Unknown"} {resultString} {AwayTeamName ?? "Unknown"} - Date: {MatchDate.ToShortDateString()}, Stadium: {StadiumName}, City: {City}";
    }
}
