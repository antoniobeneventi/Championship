namespace Championship;

public class Matchday
{
    public int MatchdayNumber { get; }
    private readonly List<Match> _matches;

    public Matchday(int matchdayNumber)
    {
        if (matchdayNumber < 1) //controlla che la giornata non sia negativa
        {
            throw new ArgumentOutOfRangeException(nameof(matchdayNumber), "Matchday number muste be positive.");
        }

        MatchdayNumber = matchdayNumber;
        _matches = new List<Match>();
    }

    public IReadOnlyList<Match> Matches => _matches.AsReadOnly();

    public void AddMatch(Match match)
    {
        if (match is null)
        {
            throw new ArgumentNullException(nameof(match), "Cannot add a null match.");
        }

        // Controlla se esiste già una partita uguale 
        if (_matches.Any(m => m.Equals(match)))
        {
            throw new DuplicateMatchException("A match with the same teams already exists in this matchday.");
        }

        _matches.Add(match);
    }



    public override string ToString()
    {
        var matchesInfo = string.Join("\n", _matches.Select(m => m.ToString()));
        return $"MatchDay {MatchdayNumber}:\n{matchesInfo}";
    }
}








