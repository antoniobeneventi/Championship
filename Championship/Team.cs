

using System.ComponentModel.DataAnnotations;

namespace Championship;

public class Team
{
    [Key]
    public int Id { get; set; } 

    public string SquadName { get; set; }
    public int FondationYear { get; set; }
    public string City { get; set; }
    public string ColorOfClub { get; set; }
    public string StadiumName { get; set; }

  
    public Team() { }

    public Team(string squadName, int fondationYear, string city, string colorOfClub, string stadiumName)
    {
        if (string.IsNullOrWhiteSpace(squadName)) 
        {
            throw new ArgumentException("Squad name cannot be null or empty.", nameof(squadName));
        }
        SquadName = squadName;

        if (fondationYear <= 0) 
        {
            throw new ArgumentOutOfRangeException(nameof(fondationYear), "Foundation year must be positive.");
        }
        FondationYear = fondationYear;

        if (string.IsNullOrWhiteSpace(city)) 
        {
            throw new ArgumentException("City cannot be null or empty.", nameof(city));
        }
        City = city;

        if (string.IsNullOrWhiteSpace(colorOfClub)) 
        {
            throw new ArgumentException("Color of the club cannot be null or empty.", nameof(colorOfClub));
        }
        ColorOfClub = colorOfClub;

        if (string.IsNullOrWhiteSpace(stadiumName)) 
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
