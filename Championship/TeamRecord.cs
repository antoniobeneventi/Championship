using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Championship;

public class TeamRecord
{
    public string SquadName { get; }
    public int FondationYear { get; }
    public string City { get; }
    public string ColorOfClub { get; }
    public string StadiumName { get; }


    public TeamRecord(string squadName, int fondationYear, string city, string colorOfClub, string stadiumName)
    {
        SquadName = squadName;
        FondationYear = fondationYear;
        City = city;
        ColorOfClub = colorOfClub;
        StadiumName = stadiumName;
    }
    public override string ToString()
    {
        return $"{SquadName} - Foundation Year: {FondationYear}, City: {City}, Colors {ColorOfClub}, Stadium: {StadiumName}";
    }
}
