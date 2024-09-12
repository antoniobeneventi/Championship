using Championship;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ChampionshipWebApp.Controllers
{
    public class TeamsController : Controller
    {
        public static List<Team> teams = new List<Team>();
      
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
        public IActionResult TeamList()
        {
            return View(teams);
        }
    }
}
