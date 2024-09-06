using System.Text;

namespace Championship;

public class Calendar
{
    private readonly List<Matchday> _matchdays;

    public Calendar()
    {
        _matchdays = new List<Matchday>();
    }

    public IReadOnlyList<Matchday> Matchdays => _matchdays.AsReadOnly();

    internal void AddMatchday(Matchday matchday)
    {
        if (matchday is null) //controlla che il matchday non sia nullo
        {
            throw new ArgumentNullException(nameof(matchday), "Cannot add a null matchday.");
        }

        if (_matchdays.Any(md => md.MatchdayNumber == matchday.MatchdayNumber)) //controlla che non ci siano due giornate uguali
        {
            throw new DuplicateMatchdayException($"A matchday with number {matchday.MatchdayNumber} already exists.");
        }
        // Controllo se le giornate sono inserite in ordine
        if (_matchdays.Count > 0)
        {
            var lastMatchdayNumber = _matchdays.Max(md => md.MatchdayNumber);
            if (matchday.MatchdayNumber != lastMatchdayNumber + 1)
            {
                throw new InvalidOperationException($"Matchday number must be {lastMatchdayNumber + 1}. The previous matchday has not been added.");
            }
        }

        _matchdays.Add(matchday);

    }
    public override string ToString()
    {
        var calendarString = new StringBuilder();
        foreach (var matchday in _matchdays)
        {
            calendarString.AppendLine(matchday.ToString());
            calendarString.AppendLine();
        }
        return calendarString.ToString();
    }
}
