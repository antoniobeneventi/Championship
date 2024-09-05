using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Championship;

public class LeagueStandings
{
    public List<TeamStanding> Standings { get; private set; }

    public LeagueStandings(List<TeamStanding> standings)
    {
        if (standings is null)
        {
            throw new ArgumentNullException(nameof(standings), "standings cannot be null");
        }
        Standings = standings;
    }

    // Metodo per aggiungere una squadra alla classifica
    public void AddTeamStanding(TeamStanding teamStanding)
    {
        if (teamStanding is null)
        {
            throw new ArgumentNullException(nameof(teamStanding), "Team standing cannot null");
        }
        Standings.Add(teamStanding);
    }

    // Metodo per ordinare la classifica
    public void SortStandings()
    {
         Standings = Standings.OrderByDescending(ts => ts.Stats.Points)
                              .ThenByDescending(ts => ts.Stats.GoalsFor - ts.Stats.GoalsAgainst)
                              .ThenByDescending(ts => ts.Stats.Wins)
                              .ThenByDescending(ts => ts.Stats.Draws)
                              .ThenByDescending(ts => ts.Stats.Losses)
                              .ThenByDescending(ts => ts.Stats.GoalsFor)
                              .ThenByDescending(ts => ts.Stats.GoalsAgainst)
                              .ToList();
    }
}
