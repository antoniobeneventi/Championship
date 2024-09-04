namespace Championship;

public class League
{
    private List<TeamRecord> Teams { get; }


    public League()
    {
        Teams = new List<TeamRecord>();

    }

    public void AddTeam(TeamRecord team)
    {
        Teams.Add(team);
    }

    public void ShowTeams()
    {
        Console.WriteLine("Teams partecipating in the league:");
        foreach (var team in Teams)
        {
            Console.WriteLine(team.ToString());
        }
    }


}

