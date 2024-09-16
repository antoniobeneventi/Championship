using Microsoft.AspNetCore.Mvc;
using ChampionshipWebApp.Data;
using Microsoft.EntityFrameworkCore;
using Championship;

namespace ChampionshipWebApp.Controllers
{
    public class TeamsController : Controller
    {
        private readonly FootballLeagueContext _context;

        public TeamsController(FootballLeagueContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddTeam(string SquadName, int FondationYear, string City, string ColorOfClub, string StadiumName)
        {
            if (!await _context.Teams.AnyAsync(t => t.SquadName == SquadName) &&
                !string.IsNullOrWhiteSpace(SquadName) &&
                !string.IsNullOrWhiteSpace(City) &&
                !string.IsNullOrWhiteSpace(ColorOfClub) &&
                !string.IsNullOrWhiteSpace(StadiumName) &&
                FondationYear > 0)
            {
                var newTeam = new Team(SquadName, FondationYear, City, ColorOfClub, StadiumName);
                _context.Teams.Add(newTeam);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View("Error");
        }

        // Metodo per visualizzare la squadra da modificare
        [HttpGet]
        public async Task<IActionResult> Edit(string squadName)
        {
            var team = await _context.Teams.FirstOrDefaultAsync(t => t.SquadName == squadName);
            if (team == null)
            {
                return NotFound();
            }
            return View(team);
        }

        // Metodo per salvare le modifiche di una squadra
        [HttpPost]
        public async Task<IActionResult> Edit(Team team)
        {
            if (ModelState.IsValid)
            {
                var existingTeam = await _context.Teams.FirstOrDefaultAsync(t => t.SquadName == team.SquadName);
                if (existingTeam != null)
                {
                    existingTeam.FondationYear = team.FondationYear;
                    existingTeam.City = team.City;
                    existingTeam.ColorOfClub = team.ColorOfClub;
                    existingTeam.StadiumName = team.StadiumName;

                    _context.Teams.Update(existingTeam);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Home");
                }
                return View("Error");
            }
            return View(team);
        }

        // Metodo per eliminare una squadra dal database
        [HttpPost]
        public async Task<IActionResult> Delete(string squadName)
        {
            var team = await _context.Teams.FirstOrDefaultAsync(t => t.SquadName == squadName);
            if (team != null)
            {
                _context.Teams.Remove(team);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Home");
        }

        // Metodo per visualizzare il calendario
        [HttpGet]
        public async Task<IActionResult> ViewCalendar()
        {
            var teams = await _context.Teams.ToListAsync();
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

            var calendar = GenerateCalendar(teams);
            return View("Calendar", calendar);
        }

        private List<List<Match>> GenerateCalendar(List<Team> teams)
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
        public async Task<IActionResult> GenerateResults()
        {
            var teams = await _context.Teams.ToListAsync();
            var calendar = GenerateCalendar(teams);

            foreach (var matchday in calendar)
            {
                foreach (var match in matchday)
                {
                    Random random = new Random();
                    int homeGoals = random.Next(0, 4);
                    int awayGoals = random.Next(0, 4);

                    var result = new MatchResult(homeGoals, awayGoals);
                    match.SetResult(result);

                    // Puoi decidere di salvare i risultati nel database se necessario
                }
            }

            // Passa il calendario con i risultati alla vista
            return View("Calendar", calendar);
        }

        [HttpPost]
        public IActionResult Rankings()
        {
            var rankings = GenerateRankings();
            return View("Rankings", rankings);
        }

        private Dictionary<Team, TeamStats> GenerateRankings()
        {
            var rankings = new Dictionary<Team, TeamStats>();

            var teams = _context.Teams.ToList();
            Console.WriteLine($"Retrieved {teams.Count} teams from the database.");

            // Initialize all teams with empty statistics
            foreach (var team in teams)
            {
                rankings[team] = new TeamStats();
            }

            var calendar = GenerateCalendar(teams);
            Console.WriteLine($"Generated {calendar.Count} matchdays.");

            foreach (var matchday in calendar)
            {
                foreach (var match in matchday)
                {
                    if (match.Result != null)
                    {
                        var homeTeamStats = rankings[match.HomeTeam];
                        var awayTeamStats = rankings[match.AwayTeam];

                        Console.WriteLine($"Processing match: {match.HomeTeam.SquadName} vs {match.AwayTeam.SquadName}");

                        homeTeamStats.GamesPlayed++;
                        awayTeamStats.GamesPlayed++;

                        homeTeamStats.GoalsFor += match.Result.HomeTeamScore;
                        homeTeamStats.GoalsAgainst += match.Result.AwayTeamScore;

                        awayTeamStats.GoalsFor += match.Result.AwayTeamScore;
                        awayTeamStats.GoalsAgainst += match.Result.HomeTeamScore;

                        if (match.Result.HomeTeamScore > match.Result.AwayTeamScore)
                        {
                            homeTeamStats.Wins++;
                            homeTeamStats.Points += 3;
                            awayTeamStats.Losses++;
                        }
                        else if (match.Result.HomeTeamScore < match.Result.AwayTeamScore)
                        {
                            awayTeamStats.Wins++;
                            awayTeamStats.Points += 3;
                            homeTeamStats.Losses++;
                        }
                        else
                        {
                            homeTeamStats.Draws++;
                            awayTeamStats.Draws++;
                            homeTeamStats.Points += 1;
                            awayTeamStats.Points += 1;
                        }
                    }
                }
            }

            return rankings;
        }

    }
}






