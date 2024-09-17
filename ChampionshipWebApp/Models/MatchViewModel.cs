namespace ChampionshipWebApp.Models;

public class MatchViewModel
{
    public int Id { get; set; }
    public string HomeTeamName { get; set; }
    public string AwayTeamName { get; set; }
    public DateTime MatchData { get; set; }
    public string StadiumName { get; set; }
    public string City { get; set; }
    public int HomeTeamScore { get; set; }
    public int AwayTeamScore { get; set; }

}
