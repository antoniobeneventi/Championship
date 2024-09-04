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

    public Match(TeamRecord homeTeam, TeamRecord awayTeam, DateTime matchDate, string stadiumName, string city)
    {
        HomeTeam = homeTeam;
        AwayTeam = awayTeam;
        MatchDate = matchDate;
        StadiumName = stadiumName;
        City = city;
    }

    public override string ToString()
    {
        return $"{HomeTeam.SquadName} vs {AwayTeam.SquadName} - Date: {MatchDate.ToShortDateString()}, Stadium: {StadiumName}, City:{City}";
    }
}
