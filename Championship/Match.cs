using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Championship;
public class Match
{
    public TeamRecord HomeTeam { get; }
    public TeamRecord AwayTeam { get; }
    public DateTime MatchDate { get; }
    public string StadiumName { get; }
    public string City { get; }
    public MatchResult? Result { get; private set; }

    public Match(TeamRecord homeTeam, TeamRecord awayTeam, DateTime matchDate, string stadiumName, string city)
    {
        HomeTeam = homeTeam;
        AwayTeam = awayTeam;
        MatchDate = matchDate;
        StadiumName = stadiumName;
        City = city;
        Result = null;
    }

    public void SetResult(MatchResult result)
    {
        if (result == null)
        {
            throw new ArgumentNullException(nameof(result), "Il risultato della partita non può essere nullo.");
        }

        Result = result;
    }

    public override string ToString()
    {
        string resultString = Result != null ? $"{Result.HomeTeamScore} - {Result.AwayTeamScore}" : "vs";
        return $"{HomeTeam.SquadName} {resultString} {AwayTeam.SquadName} - Data: {MatchDate.ToShortDateString()}, Stadio: {StadiumName}, Città: {City}";
    }
}
