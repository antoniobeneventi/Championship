using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Championship.Models;
using Championship;


namespace MatchResultsGeneratorConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Configura il contesto del database
            var optionsBuilder = new DbContextOptionsBuilder<FootballLeagueContext>();
            optionsBuilder.UseSqlite("Data Source=FootballLeague.db"); // Modifica con la tua stringa di connessione

            using (var context = new FootballLeagueContext(optionsBuilder.Options))
            {
                var generator = new MatchResultsGenerator(context);
                generator.GenerateResults();
            }

            Console.WriteLine("Match results generated successfully.");
        }
    }
}
