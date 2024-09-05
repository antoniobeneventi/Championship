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

    public override string ToString()
    {
        var teamsInfo = string.Join("\n", Teams.Select(t => t.ToString()));
        return $"Teams participating in the league:\n{teamsInfo}";
    }


}

