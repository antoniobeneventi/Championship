using Microsoft.EntityFrameworkCore;
using ChampionshipWebApp.Models;
using Championship;

using Microsoft.EntityFrameworkCore;
using ChampionshipWebApp.Models;
using Championship;

namespace ChampionshipWebApp.Data
{
    public class FootballLeagueContext : DbContext
    {
        public FootballLeagueContext(DbContextOptions<FootballLeagueContext> options)
            : base(options)
        {
        }

        // Definizione del DbSet per le squadre
        public DbSet<Team> Teams { get; set; }

        // Definizione del DbSet per altre entità
        //public DbSet<Match> Matches { get; set; }
        //public DbSet<MatchResult> MatchResults { get; set; }
        //public DbSet<TeamStats> TeamStats { get; set; }

        // Configurazione del modello di database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurazioni per l'entità Team
            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.SquadName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.City).IsRequired().HasMaxLength(100);
                entity.Property(e => e.ColorOfClub).IsRequired().HasMaxLength(50);
                entity.Property(e => e.StadiumName).IsRequired().HasMaxLength(100);
                // Ulteriori configurazioni, se necessarie
            });

            //// Configurazioni per l'entità Match
            //modelBuilder.Entity<Match>(entity =>
            //{
            //    entity.HasKey(e => e.Id);
            //    entity.Property(e => e.HomeTeam).IsRequired();
            //    entity.Property(e => e.AwayTeam).IsRequired();
            //    entity.Property(e => e.MatchDate).IsRequired();
            //    entity.Property(e => e.StadiumName).IsRequired().HasMaxLength(100);
            //    entity.Property(e => e.City).IsRequired().HasMaxLength(100);
            //    // Ulteriori configurazioni, se necessarie
            //});

            //// Configurazioni per l'entità MatchResult
            //modelBuilder.Entity<MatchResult>(entity =>
            //{
            //    entity.HasKey(e => e.Id);
            //    entity.Property(e => e.HomeTeamScore).IsRequired();
            //    entity.Property(e => e.AwayTeamScore).IsRequired();
            //    // Ulteriori configurazioni, se necessarie
            //});

            //// Configurazioni per l'entità TeamStats
            //modelBuilder.Entity<TeamStats>(entity =>
            //{
            //    entity.HasKey(e => e.TeamId);
            //    entity.Property(e => e.GamesPlayed).IsRequired();
            //    entity.Property(e => e.Wins).IsRequired();
            //    entity.Property(e => e.Draws).IsRequired();
            //    entity.Property(e => e.Losses).IsRequired();
            //    entity.Property(e => e.Points).IsRequired();
            //    entity.Property(e => e.GoalsFor).IsRequired();
            //    entity.Property(e => e.GoalsAgainst).IsRequired();
            //    entity.Property(e => e.GoalDifference).IsRequired();
            //    // Ulteriori configurazioni, se necessarie
            //});
        }
    }
}
