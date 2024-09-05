using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Championship;

public class Team
{
    public string SquadName { get; }
    public int FondationYear { get; }
    public string City { get; }
    public string ColorOfClub { get; }
    public string StadiumName { get; }


    public Team(string squadName, int fondationYear, string city, string colorOfClub, string stadiumName)
    {
        if (string.IsNullOrWhiteSpace(squadName)) // controllo su SquadName
        {
            throw new ArgumentException("Squad name cannot be null or empty.", nameof(squadName));
        }
        SquadName = squadName;

        if (fondationYear <= 0) // controllo su FondationYear
        {
            throw new ArgumentOutOfRangeException(nameof(fondationYear), "Foundation year must be positive.");
        }
        FondationYear = fondationYear;

        if (string.IsNullOrWhiteSpace(city)) // controllo su City
        {
            throw new ArgumentException("City cannot be null or empty.", nameof(city));
        }
        City = city;

        if (string.IsNullOrWhiteSpace(colorOfClub)) // controllo su ColorOfClub
        {
            throw new ArgumentException("Color of the club cannot be null or empty.", nameof(colorOfClub));
        }
        ColorOfClub = colorOfClub;

        if (string.IsNullOrWhiteSpace(stadiumName)) // controllo su StadiumName
        {
            throw new ArgumentException("Stadium name cannot be null or empty.", nameof(stadiumName));
        }
        StadiumName = stadiumName;
    }
    public override string ToString()
    {
        return $"{SquadName} - Foundation Year: {FondationYear}, City: {City}, Colors {ColorOfClub}, Stadium: {StadiumName}";
    }
}
