using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Championship;

public class LeagueStandings
{
    public List<TeamStanding> Standings { get; }

    public LeagueStandings(List<TeamStanding> standings)
    {
        Standings = standings ?? throw new ArgumentNullException(nameof(standings), "standigs cannot be null");
    }

    // Metodo per aggiungere una squadra alla classifica
    public void AddTeamStanding(TeamStanding teamStanding)
    {
        if (teamStanding is null)
        {
            throw new ArgumentNullException(nameof(teamStanding), "Team Standing cannot be null");
        }

        Standings.Add(teamStanding);
    }

}
