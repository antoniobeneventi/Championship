using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Championship;
public class Match
{
    public Team HomeTeam { get; }
    public Team AwayTeam { get; }
    public DateTime MatchDate { get; }
    public string StadiumName { get; }
    public string City { get; }
    public MatchResult? Result { get; private set; }


    public Match(Team homeTeam, Team awayTeam, DateTime matchDate, string stadiumName, string city)
    {
        HomeTeam = homeTeam ?? throw new ArgumentNullException(nameof(homeTeam), "Home team cannot be null.");
        AwayTeam = awayTeam ?? throw new ArgumentNullException(nameof(awayTeam), "Away team cannot be null.");
        MatchDate = matchDate;

        if (string.IsNullOrWhiteSpace(stadiumName)) // controllo su StadiumName
        {
            throw new ArgumentException("Stadium name cannot be null or empty.", nameof(stadiumName));
        }
        StadiumName = stadiumName;

        if (string.IsNullOrWhiteSpace(city)) // controllo su City
        {
            throw new ArgumentException("City cannot be null or empty.", nameof(city));
        }
        City = city;

        Result = null;
    }


    public void SetResult(MatchResult result)
    {
        if (result is null)
        {
            throw new ArgumentNullException(nameof(result), "The result cannot be null");
        }

        Result = result;

    }
    public override bool Equals(object obj)
    {
        if (obj is Match otherMatch)
        {
            // Confronta se le squadre sono le stesse, indipendentemente da casa o trasferta
            bool sameTeams = (HomeTeam.Equals(otherMatch.HomeTeam) && AwayTeam.Equals(otherMatch.AwayTeam)) ||
                             (HomeTeam.Equals(otherMatch.AwayTeam) && AwayTeam.Equals(otherMatch.HomeTeam));
            return sameTeams;
        }
        return false;
    }

    public override int GetHashCode()
    {
        
        int homeTeamHash = HomeTeam.GetHashCode();
        int awayTeamHash = AwayTeam.GetHashCode();
        return homeTeamHash + awayTeamHash;
    }




    public override string ToString()
    {
        string resultString = Result != null ? $"{Result.HomeTeamScore} - {Result.AwayTeamScore}" : "vs";
        return $"{HomeTeam.SquadName} {resultString} {AwayTeam.SquadName} - Date: {MatchDate.ToShortDateString()}, Stadium: {StadiumName}, City: {City}";
    }
}
