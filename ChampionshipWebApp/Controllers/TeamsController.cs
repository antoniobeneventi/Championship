using Championship;
using Microsoft.AspNetCore.Mvc;



namespace YourProjectNamespace.Controllers;


public class TeamsController : Controller
{

    private static List<Team> teams = new List<Team>();

    [HttpPost]
    public IActionResult AddTeam(string SquadName, int FondationYear, string City, string ColorOfClub, string NameOfStadium)
    {

        if (!string.IsNullOrWhiteSpace(SquadName) && !string.IsNullOrWhiteSpace(City) && !string.IsNullOrWhiteSpace(ColorOfClub) && !string.IsNullOrWhiteSpace(NameOfStadium) && FondationYear > 0)
        {
            var newTeam = new Team(SquadName, FondationYear, City, ColorOfClub, NameOfStadium);
            teams.Add(newTeam);

            return RedirectToAction("TeamList");
        }

        return View("Error");
    }



    [HttpGet]
    public IActionResult TeamList()
    {

        return View(teams);
    }
}


