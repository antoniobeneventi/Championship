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
        
        foreach (var matchday in _matchdays)
        {
            Console.WriteLine(matchday.ToString());
            Console.WriteLine();
        }
    }

    internal void AddMatchday(Matchday matchday)
    {
        _matchdays.Add(matchday);
    }
}
