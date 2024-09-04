using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Championship;

public class Matchday
{
    public int MatchdayNumber { get; }
    private List<Match> Matches { get; }

    public Matchday(int matchdayNumber)
    {
        MatchdayNumber = matchdayNumber;
        Matches = new List<Match>();
    }

    public void AddMatch(Match match)
    {
        Matches.Add(match);
    }
    public bool IsStadiumUsed(string stadiumName)
    {
        return Matches.Any(m => m.StadiumName == stadiumName);
    }


    public override string ToString()
    {
        var matchesInfo = string.Join("\n", Matches.Select(m => m.ToString()));
        return $"Matchday {MatchdayNumber}:\n{matchesInfo}";
    }
}

