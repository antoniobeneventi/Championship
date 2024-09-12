using Championship;
using Microsoft.AspNetCore.Mvc;


namespace ChampionshipWebApp.Controllers
{
    public class TeamsController : Controller
    {
        public static List<Team> teams = new List<Team>();

        [HttpPost]
        public IActionResult AddTeam(string SquadName, int FondationYear, string City, string ColorOfClub, string NameOfStadium)
        {
            if (teams.Any(t => t.SquadName.Equals(SquadName, System.StringComparison.OrdinalIgnoreCase)))
            {
                ViewBag.ErorrMessage = "A team with this name alredy exists";
                return View("error");
            }
            if (!string.IsNullOrWhiteSpace(SquadName) && FondationYear > 0 && !string.IsNullOrWhiteSpace(City) && !string.IsNullOrWhiteSpace(ColorOfClub) && !string.IsNullOrWhiteSpace(NameOfStadium))
            {
                var newTeam = new Team(SquadName, FondationYear, City, ColorOfClub, NameOfStadium);
                teams.Add(newTeam);

                // Reindirizza alla pagina principale dopo aver aggiunto una squadra
                return RedirectToAction("Index", "Home");
            }

            return View("Error");
        }

        [HttpGet]
        public IActionResult TeamList()
        {
            return View(teams);
        }
    }
}

