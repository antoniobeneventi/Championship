using Championship;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ChampionshipWebApp.Controllers
{
    public class TeamsController : Controller
    {
        public static List<Team> teams = new List<Team>();

        // Metodo esistente per aggiungere una squadra
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
            var matchDays = 6; // Numero di giornate

            for (int i = 0; i < matchDays; i++)
            {
                var matchday = new List<Match>();

                for (int j = 0; j < teams.Count / 2; j++)
                {
                    var homeTeam = teams[j];
                    var awayTeam = teams[teams.Count - 1 - j];
                    var match = new Match(homeTeam, awayTeam, DateTime.Now.AddDays(i), homeTeam.StadiumName, homeTeam.City);
                    matchday.Add(match);
                }

                calendar.Add(matchday);
                teams.Reverse(1, teams.Count - 1); // Rotazione delle squadre
            }

            return calendar;
        }
    }
}
