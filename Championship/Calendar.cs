namespace Championship;

public class Calendar
{
    private List<Matchday> Matchdays { get;}

    public Calendar()
    {
        Matchdays = new List<Matchday>();
    }

    public void AddMatchday(Matchday matchday)
    {
        Matchdays.Add(matchday);
    }

    public void ShowCalendar()
    {
        Console.WriteLine("League Calendar:");
        foreach (var matchday in Matchdays)
        {
            Console.WriteLine(matchday.ToString());
            Console.WriteLine(); 
        }
    }
}


