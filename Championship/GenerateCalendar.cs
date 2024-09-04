using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Championship;

public class CalendarGenerator
{
    public Calendar GenerateCalendar(List<TeamRecord> teams)
    {
        int numTeams = teams.Count;
        int numMatchdays = numTeams - 1;
        Calendar calendar = new Calendar();
        DateTime currentDate = DateTime.Now;

        //giornate d'andata
        for (int i = 0; i < numMatchdays; i++)
        {
            Matchday matchday = new Matchday(i + 1);

            for (int j = 0; j < numTeams / 2; j++)
            {
                TeamRecord homeTeam = teams[j];
                TeamRecord awayTeam = teams[numTeams - 1 - j];
                string stadiumName;
                string city;
                if (i % 2 == 0)
                {
                    stadiumName = homeTeam.StadiumName;
                    city = homeTeam.City;
                    if (!matchday.IsStadiumUsed(stadiumName))
                    {
                        matchday.AddMatch(new Match(homeTeam, awayTeam, currentDate.AddDays(i * 7), stadiumName, city));
                    }
                    else
                    {
                        stadiumName = awayTeam.StadiumName;
                        city = awayTeam.City;
                        matchday.AddMatch(new Match(awayTeam, homeTeam, currentDate.AddDays(i * 7), stadiumName, city));
                    }
                }
                else
                {
                    stadiumName = awayTeam.StadiumName;
                    city = awayTeam.City;
                    if (!matchday.IsStadiumUsed(stadiumName))
                    {
                        matchday.AddMatch(new Match(awayTeam, homeTeam, currentDate.AddDays(i * 7), stadiumName, city));
                    }
                    else
                    {
                        stadiumName = homeTeam.StadiumName;
                        city = homeTeam.City;
                        matchday.AddMatch(new Match(homeTeam, awayTeam, currentDate.AddDays(i * 7), stadiumName, city));
                    }
                }
            }

            
            TeamRecord lastTeam = teams[numTeams - 1];
            teams.RemoveAt(numTeams - 1);
            teams.Insert(1, lastTeam);

            calendar.AddMatchday(matchday);
        }

        //  giornate di ritorno
        for (int i = 0; i < numMatchdays; i++)
        {
            Matchday matchday = new Matchday(numMatchdays + i + 1);

            for (int j = 0; j < numTeams / 2; j++)
            {
                TeamRecord homeTeam = teams[j];
                TeamRecord awayTeam = teams[numTeams - 1 - j];
                string stadiumName;
                string city;

                if (i % 2 == 0)
                {
                    stadiumName = awayTeam.StadiumName;
                    city = awayTeam.City;
                    if (!matchday.IsStadiumUsed(stadiumName))
                    {
                        matchday.AddMatch(new Match(awayTeam, homeTeam, currentDate.AddDays((numMatchdays + i) * 7), stadiumName, city));
                    }
                    else
                    {
                        stadiumName = homeTeam.StadiumName;
                        city = homeTeam.City;
                        matchday.AddMatch(new Match(homeTeam, awayTeam, currentDate.AddDays((numMatchdays + i) * 7), stadiumName, city));
                    }
                }
                else
                {
                    stadiumName = homeTeam.StadiumName;
                    city = homeTeam.City;
                    if (!matchday.IsStadiumUsed(stadiumName))
                    {
                        matchday.AddMatch(new Match(homeTeam, awayTeam, currentDate.AddDays((numMatchdays + i) * 7), stadiumName, city));
                    }
                    else
                    {
                        stadiumName = awayTeam.StadiumName;
                        city = awayTeam.City;
                        matchday.AddMatch(new Match(awayTeam, homeTeam, currentDate.AddDays((numMatchdays + i) * 7), stadiumName, city));
                    }
                }
            }

            
            TeamRecord lastTeam = teams[numTeams - 1];
            teams.RemoveAt(numTeams - 1);
            teams.Insert(1, lastTeam);

            calendar.AddMatchday(matchday);
        }

        return calendar;
    }
}
