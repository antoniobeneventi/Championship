using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Championship;

public class MatchDetails
{
    public int MatchID { get; set; }
    public int MatchdayID { get; set; }
    public string HomeTeamName { get; set; }
    public string AwayTeamName { get; set; }
    public int HomeTeamScore { get; set; }
    public int AwayTeamScore { get; set; }


    public override string ToString()
    {
        return $"MatchID: {MatchID}, MatchdayID: {MatchdayID}, {HomeTeamName} {HomeTeamScore} - {AwayTeamScore} {AwayTeamName}";
    }
}





