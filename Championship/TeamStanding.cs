using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Championship;

public class TeamStanding
{
    public Team Team { get; }
    public TeamStats Stats { get; }

    public TeamStanding(Team team, TeamStats stats)
    {
        Team = team ?? throw new ArgumentNullException(nameof(team), "Team cannot be null");
        Stats = stats ?? throw new ArgumentNullException(nameof(stats), "Stats cannot be null");
    }

    public override string ToString()
    {
        return $"{Team.SquadName} - {Stats}";
    }
}
