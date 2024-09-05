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
