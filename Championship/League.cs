namespace Championship;

public class League
{
    private List<Team> Teams { get; }


    public League()
    {
        Teams = new List<Team>();

    }

    public void AddTeam(Team team)
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

