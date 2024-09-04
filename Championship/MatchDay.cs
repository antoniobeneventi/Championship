namespace Championship;

public class Matchday
{
    public int MatchdayNumber { get; }
    private readonly List<Match> _matches;

    public Matchday(int matchdayNumber)
    {
        if (matchdayNumber < 0) //controlla che la giornata non sia negativa
        {
            throw new ArgumentOutOfRangeException(nameof(matchdayNumber), "Matchday number cannot be negative.");
        }

        MatchdayNumber = matchdayNumber;
        _matches = new List<Match>();
    }

    public IReadOnlyList<Match> Matches => _matches.AsReadOnly();

    public void AddMatch(Match match)
    {
        if (match == null) //controlla che la partita non sia nulla
        {
            throw new ArgumentNullException(nameof(match), "Cannot add a null match.");
        }
        //controlla che non ci siano partite uguali
        if (_matches.Any(m =>
            (m.HomeTeam.SquadName == match.HomeTeam.SquadName && m.AwayTeam.SquadName == match.AwayTeam.SquadName) ||
            (m.HomeTeam.SquadName == match.AwayTeam.SquadName && m.AwayTeam.SquadName == match.HomeTeam.SquadName)
        ))
        {
            throw new InvalidOperationException("A match with the same teams already exists in this matchday.");
        }

        _matches.Add(match);
    }

    public override string ToString()
    {
        var matchesInfo = string.Join("\n", _matches.Select(m => m.ToString()));
        return $"MatchDay {MatchdayNumber}:\n{matchesInfo}";
    }
}








