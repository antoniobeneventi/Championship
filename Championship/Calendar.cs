namespace Championship;

public class Calendar
{
    private readonly List<Matchday> _matchdays;

    public Calendar()
    {
        _matchdays = new List<Matchday>();
    }

    public IReadOnlyList<Matchday> Matchdays => _matchdays.AsReadOnly();

    public void ShowCalendar()
    {
        if (_matchdays == null) //controlla se la lista non sia nulla
        {
            throw new InvalidOperationException("Matchdays list is null.");
        }

        foreach (var matchday in _matchdays)
        {
            Console.WriteLine(matchday.ToString());
            Console.WriteLine();
        }
    }

    internal void AddMatchday(Matchday matchday)
    {
        if (matchday == null) //controlla che il matchday non sia nullo
        {
            throw new ArgumentNullException(nameof(matchday), "Cannot add a null matchday.");
        }

        if (_matchdays.Any(md => md.MatchdayNumber == matchday.MatchdayNumber)) //controlla che non ci siano due giornate uguali
        {
            throw new InvalidOperationException($"A matchday with number {matchday.MatchdayNumber} already exists.");
        }

        _matchdays.Add(matchday);

    }
}
