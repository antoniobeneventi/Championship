namespace Championship;

public class Matchday
{
    public int MatchdayNumber { get; }
    private readonly List<Match> _matches;

    public Matchday(int matchdayNumber)
    {
        MatchdayNumber = matchdayNumber;
        _matches = new List<Match>();
    }

    public IReadOnlyList<Match> Matches => _matches.AsReadOnly();

    public void AddMatch(Match match)
    {
        _matches.Add(match);
    }

    public bool IsStadiumUsed(string stadiumName)
    {
        return _matches.Any(m => m.StadiumName == stadiumName);
    }

    public override string ToString()
    {
        var matchesInfo = string.Join("\n", _matches.Select(m => m.ToString()));
        return $"MatchDay {MatchdayNumber}:\n{matchesInfo}";
    }
}







