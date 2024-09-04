using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Championship;
//public class GenerateCalendarWithResult
//{
//    private static readonly Random Random = new Random(2024); 

//    public Calendar GenerateCalendarWithScores(List<TeamRecord> teams)
//    {
//        int numTeams = teams.Count;
//        int numMatchdays = numTeams - 1;
//        Calendar calendar = new Calendar();
//        DateTime currentDate = DateTime.Now;

        
//        int GenerateRandomScore()
//        {
//            return Random.Next(0, 5); 
//        }

//        MatchResult GenerateMatchWithResult(TeamRecord homeTeam, TeamRecord awayTeam, DateTime matchDate)
//        {
//            int homeScore = GenerateRandomScore();
//            int awayScore = GenerateRandomScore();
//            string stadiumName;
//            string city;

//            if (homeScore > awayScore)
//            {
//                stadiumName = homeTeam.StadiumName;
//                city = homeTeam.City;
//            }
//            else
//            {
//                stadiumName = awayTeam.StadiumName;
//                city = awayTeam.City;
//            }

//            return new MatchWithResult(homeTeam, awayTeam, matchDate, homeScore, awayScore, stadiumName, city);
//        }

//        // giornate d'andata
//        for (int i = 0; i < numMatchdays; i++)
//        {
//            Matchday matchday = new Matchday(i + 1);

//            for (int j = 0; j < numTeams / 2; j++)
//            {
//                TeamRecord homeTeam = teams[j];
//                TeamRecord awayTeam = teams[numTeams - 1 - j];
//                DateTime matchDate = currentDate.AddDays(i * 7);

//                MatchResult matchWithResult = GenerateMatchWithResult(homeTeam, awayTeam, matchDate);

//                matchday.AddMatch(matchWithResult);
//            }

            
//            TeamRecord lastTeam = teams[numTeams - 1];
//            teams.RemoveAt(numTeams - 1);
//            teams.Insert(1, lastTeam);

//            calendar.AddMatchday(matchday);
//        }

//        // giornate di ritorno
//        for (int i = 0; i < numMatchdays; i++)
//        {
//            Matchday matchday = new Matchday(numMatchdays + i + 1);

//            for (int j = 0; j < numTeams / 2; j++)
//            {
//                TeamRecord homeTeam = teams[j];
//                TeamRecord awayTeam = teams[numTeams - 1 - j];
//                DateTime matchDate = currentDate.AddDays((numMatchdays + i) * 7);

//                MatchResult matchWithResult = GenerateMatchWithResult(homeTeam, awayTeam, matchDate);

//                matchday.AddMatch(matchWithResult);
//            }

//            TeamRecord lastTeam = teams[numTeams - 1];
//            teams.RemoveAt(numTeams - 1);
//            teams.Insert(1, lastTeam);

//            calendar.AddMatchday(matchday);
//        }

//        return calendar;
//    }
//}