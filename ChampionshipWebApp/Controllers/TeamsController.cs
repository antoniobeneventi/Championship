using Championship;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ChampionshipWebApp.Controllers
{
    public class TeamsController : Controller
    {
        public static List<Team> teams = new List<Team>();
        public static List<List<Match>> GeneratedCalendarWithResults = new List<List<Match>>();


        [HttpPost]
        public IActionResult AddTeam(string SquadName, int FondationYear, string City, string ColorOfClub, string StadiumName)
        {
            if (!teams.Any(t => t.SquadName == SquadName) &&
                !string.IsNullOrWhiteSpace(SquadName) &&
                !string.IsNullOrWhiteSpace(City) &&
                !string.IsNullOrWhiteSpace(ColorOfClub) &&
                !string.IsNullOrWhiteSpace(StadiumName) &&
                FondationYear > 0)
            {
                var newTeam = new Team(SquadName, FondationYear, City, ColorOfClub, StadiumName);
                teams.Add(newTeam);
                return RedirectToAction("Index", "Home");
            }
            return View("Error");
        }

        [HttpGet]
        public IActionResult Edit(string squadName)
        {
            var team = teams.FirstOrDefault(t => t.SquadName == squadName);
            if (team == null)
            {
                return NotFound();
            }
            return View(team);
        }

        [HttpPost]
        public IActionResult Edit(Team team)
        {
            var existingTeam = teams.FirstOrDefault(t => t.SquadName == team.SquadName);
            if (existingTeam != null)
            {
                existingTeam.FondationYear = team.FondationYear;
                existingTeam.City = team.City;
                existingTeam.ColorOfClub = team.ColorOfClub;
                existingTeam.StadiumName = team.StadiumName;
                return RedirectToAction("Index", "Home");
            }
            return View("Error");
        }

        [HttpPost]
        public IActionResult Delete(string squadName)
        {
            var team = teams.FirstOrDefault(t => t.SquadName == squadName);
            if (team != null)
            {
                teams.Remove(team);
            }
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult ViewCalendar()
        {
            if (teams.Count < 2)
            {
                ViewBag.Message = "Non ci sono squadre, inseriscine almeno 2.";
                return View("Calendar");
            }
            if (teams.Count % 2 != 0)
            {
                ViewBag.Message = "Il numero delle squadre inserito è dispari, aggiungi almeno un'altra squadra.";
                return View("Calendar");
            }

            var calendar = GenerateCalendar();
            return View("Calendar", calendar);
        }

        private List<List<Match>> GenerateCalendar()
        {
            var calendar = new List<List<Match>>();
            int numTeams = teams.Count;
            int numMatchdays = numTeams - 1; 

          
            DateTime startDate = DateTime.Now;

            var rotatingTeams = new List<Team>(teams);

            for (int round = 0; round < numMatchdays; round++)
            {
                var matchday = new List<Match>();

                for (int j = 0; j < numTeams / 2; j++)
                {
                    var homeTeam = rotatingTeams[j];
                    var awayTeam = rotatingTeams[numTeams - 1 - j];

                    var matchDate = startDate.AddDays(round * 7);
                    var match = new Match(homeTeam, awayTeam, matchDate, homeTeam.StadiumName, homeTeam.City);
                    matchday.Add(match);
                }

                calendar.Add(matchday);

                var lastTeam = rotatingTeams[numTeams - 1];
                rotatingTeams.RemoveAt(numTeams - 1);
                rotatingTeams.Insert(1, lastTeam);
            }

            for (int round = 0; round < numMatchdays; round++)
            {
                var matchday = new List<Match>();

                for (int j = 0; j < numTeams / 2; j++)
                {
                    var homeTeam = rotatingTeams[numTeams - 1 - j]; 
                    var awayTeam = rotatingTeams[j];

                    var matchDate = startDate.AddDays((numMatchdays + round) * 7);
                    var match = new Match(homeTeam, awayTeam, matchDate, homeTeam.StadiumName, homeTeam.City);
                    matchday.Add(match);
                }

                calendar.Add(matchday);

                var lastTeam = rotatingTeams[numTeams - 1];
                rotatingTeams.RemoveAt(numTeams - 1);
                rotatingTeams.Insert(1, lastTeam);
            }

            return calendar;
        }


     
        [HttpPost]
        public IActionResult GenerateResults()
        {
            var calendar = GenerateCalendar();

            foreach (var matchday in calendar)
            {
                foreach (var match in matchday)
                {
                    Random random = new Random();
                    int homeGoals = random.Next(0, 4);
                    int awayGoals = random.Next(0, 4);

                    var result = new MatchResult(homeGoals, awayGoals);
                    match.SetResult(result);
                }
            }

            GeneratedCalendarWithResults = calendar;

            return View("Calendar", calendar);
        }


        [HttpPost]
        public IActionResult Rankings()
        {

            var rankings = GenerateRankings();


            return View(rankings);
        }

        private List<KeyValuePair<Team, int>> GenerateRankings()
        {
            var rankings = new Dictionary<Team, int>();

           
            foreach (var team in TeamsController.teams)
            {
                rankings[team] = 0;
            }

            var calendar = GeneratedCalendarWithResults;

            if (calendar != null && calendar.Count > 0)
            {
                foreach (var matchday in calendar)
                {
                    foreach (var match in matchday)
                    {
                        if (match.Result != null)
                        {
                            
                            if (match.Result.HomeTeamScore > match.Result.AwayTeamScore)
                            {
                                
                                rankings[match.HomeTeam] += 3;
                            }
                            else if (match.Result.HomeTeamScore < match.Result.AwayTeamScore)
                            {
                                
                                rankings[match.AwayTeam] += 3;
                            }
                            else
                            {
                                
                                rankings[match.HomeTeam] += 1;
                                rankings[match.AwayTeam] += 1;
                            }
                        }
                    }
                }
            }

            
            var orderedRankings = rankings.OrderByDescending(r => r.Value).ToList();

            return orderedRankings;
        }





    }
}

