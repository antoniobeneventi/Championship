﻿using Championship;
using System.Data.SQLite;


public class DatabaseManager
{
    private readonly string _connectionString;

    public DatabaseManager(string databasePath)
    {
        _connectionString = $"Data Source={databasePath};Version=3;";
    }

    private SQLiteConnection OpenConnection()
    {
        var connection = new SQLiteConnection(_connectionString);
        connection.Open();
        return connection;
    }
    public void CreateTables()
    {
        using var connection = OpenConnection();

        string dropTeamsTable = "DROP TABLE IF EXISTS Teams;";
        string dropMatchesTable = "DROP TABLE IF EXISTS Matches;";
        string dropMatchResultsTable = "DROP TABLE IF EXISTS MatchResults;";

        using SQLiteCommand dropCommand1 = new SQLiteCommand(dropTeamsTable, connection);
        dropCommand1.ExecuteNonQuery();

        using SQLiteCommand dropCommand2 = new SQLiteCommand(dropMatchesTable, connection);
        dropCommand2.ExecuteNonQuery();

        using SQLiteCommand dropCommand3 = new SQLiteCommand(dropMatchResultsTable, connection);
        dropCommand3.ExecuteNonQuery();

        string createTeamsTable = @"
        CREATE TABLE IF NOT EXISTS Teams (
            TeamID INTEGER PRIMARY KEY AUTOINCREMENT,
            SquadName TEXT NOT NULL UNIQUE,
            FondationYear  TEXT NOT NULL,
            City TEXT NOT NULL,
            ColorOfClub TEXT NOT NULL,
            StadiumName TEXT NOT NULL
        );";

        string createMatchesTable = @"
        CREATE TABLE IF NOT EXISTS Matches (
            MatchID INTEGER PRIMARY KEY AUTOINCREMENT,
            MatchdayID INTEGER,
            HomeTeamName TEXT NOT NULL,
            AwayTeamName TEXT NOT NULL,
            MatchDate TEXT NOT NULL,
            FOREIGN KEY (MatchdayID) REFERENCES Matchdays(MatchdayID)
        );";

        string createMatchResultsTable = @"
        CREATE TABLE IF NOT EXISTS MatchResults (
            MatchID INTEGER PRIMARY KEY AUTOINCREMENT,
            HomeTeamScore INTEGER NOT NULL,
            AwayTeamScore INTEGER NOT NULL,
            FOREIGN KEY (MatchID) REFERENCES Matches(MatchID)
        );";

        using SQLiteCommand command1 = new SQLiteCommand(createTeamsTable, connection);
        command1.ExecuteNonQuery();

        using SQLiteCommand command2 = new SQLiteCommand(createMatchesTable, connection);
        command2.ExecuteNonQuery();

        using SQLiteCommand command3 = new SQLiteCommand(createMatchResultsTable, connection);
        command3.ExecuteNonQuery();
    }
    public void InsertData()
    {
        using var connection = OpenConnection();

        string insertTeams = @"
        INSERT INTO Teams (SquadName, FondationYear, City, ColorOfClub, StadiumName) VALUES
        ('Juventus', '1897', 'Turin', 'Black and White', 'Allianz Stadium'),
        ('Milan', '1899', 'Milan', 'Red and Black', 'San Siro'),
        ('Roma', '1927', 'Rome', 'Yellow and Red', 'Stadio Olimpico'),
        ('Napoli', '1926', 'Naples', 'Blue and White', 'Stadio Diego Armando Maradona');";

        string insertMatches = @"
        INSERT INTO Matches (MatchdayID, HomeTeamName, AwayTeamName, MatchDate) VALUES
        (1, 'Juventus', 'Napoli', '2024-09-01'),
        (1, 'Milan', 'Roma', '2024-09-01'),
        (2, 'Roma', 'Juventus', '2024-09-08'),
        (2, 'Milan', 'Napoli', '2024-09-08'),
        (3, 'Juventus', 'Milan', '2024-09-15'),
        (3, 'Roma', 'Napoli', '2024-09-15'),
        (4, 'Napoli', 'Juventus', '2024-09-22'),
        (4, 'Roma', 'Milan', '2024-09-22'),
        (5, 'Juventus', 'Roma', '2024-09-29'),
        (5, 'Napoli', 'Milan', '2024-09-29'),
        (6, 'Milan', 'Juventus', '2024-10-06'),
        (6, 'Napoli', 'Roma', '2024-10-06');";

        string insertMatchResults = @"
        INSERT INTO MatchResults (MatchID, HomeTeamScore, AwayTeamScore) VALUES
        (1, 2, 1),
        (2, 1, 1),
        (3, 3, 0),
        (4, 0, 2),
        (5, 1, 2),
        (6, 2, 2),
        (7, 1, 1),
        (8, 0, 1),
        (9, 2, 1),
        (10, 1, 0),
        (11, 0, 0),
        (12, 2, 1);";

        using SQLiteCommand command1 = new SQLiteCommand(insertTeams, connection);
        command1.ExecuteNonQuery();

        using SQLiteCommand command2 = new SQLiteCommand(insertMatches, connection);
        command2.ExecuteNonQuery();

        using SQLiteCommand command3 = new SQLiteCommand(insertMatchResults, connection);
        command3.ExecuteNonQuery();
    }

    public List<TeamStanding> GetStandingsForMatchdayRange(int fromMatchday, int toMatchday)
    {
        List<TeamStanding> standings = new List<TeamStanding>();

        using var connection = OpenConnection();

        string query = @"
    WITH TeamScores AS (
        SELECT 
            t1.SquadName AS TeamName,
            SUM(CASE 
                WHEN (mr.HomeTeamScore > mr.AwayTeamScore AND m.HomeTeamName = t1.SquadName) OR 
                     (mr.HomeTeamScore < mr.AwayTeamScore AND m.AwayTeamName = t1.SquadName) THEN 1
                ELSE 0
            END) AS Wins,
            SUM(CASE 
                WHEN mr.HomeTeamScore = mr.AwayTeamScore THEN 1
                ELSE 0
            END) AS Draws,
            SUM(CASE 
                WHEN (mr.HomeTeamScore < mr.AwayTeamScore AND m.HomeTeamName = t1.SquadName) OR 
                     (mr.HomeTeamScore > mr.AwayTeamScore AND m.AwayTeamName = t1.SquadName) THEN 1
                ELSE 0
            END) AS Losses,
            SUM(CASE WHEN m.HomeTeamName = t1.SquadName THEN mr.HomeTeamScore ELSE mr.AwayTeamScore END) AS GoalsFor,
            SUM(CASE WHEN m.HomeTeamName = t1.SquadName THEN mr.AwayTeamScore ELSE mr.HomeTeamScore END) AS GoalsAgainst,
            COUNT(m.MatchID) AS GamesPlayed
        FROM 
            Teams t1
        LEFT JOIN Matches m ON t1.SquadName = m.HomeTeamName OR t1.SquadName = m.AwayTeamName
        LEFT JOIN MatchResults mr ON m.MatchID = mr.MatchID
        WHERE 
            m.MatchdayID BETWEEN @fromMatchday AND @toMatchday
        GROUP BY t1.SquadName
    )

    SELECT 
        TeamName,
        GamesPlayed,
        Wins,
        Draws,
        Losses,
        (Wins * 3 + Draws) AS Points,
        GoalsFor,
        GoalsAgainst,
        (GoalsFor - GoalsAgainst) AS GoalDifference
    FROM 
        TeamScores
    ORDER BY 
        Points DESC, GoalDifference DESC, GoalsFor DESC;";

        using var command = new SQLiteCommand(query, connection);
        command.Parameters.AddWithValue("@fromMatchday", fromMatchday);
        command.Parameters.AddWithValue("@toMatchday", toMatchday);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            standings.Add(new TeamStanding
            {
                SquadName = reader.GetString(0),
                GamesPlayed = reader.GetInt32(1),
                Wins = reader.GetInt32(2),
                Draws = reader.GetInt32(3),
                Losses = reader.GetInt32(4),
                Points = reader.GetInt32(5),
                GoalsFor = reader.GetInt32(6),
                GoalsAgainst = reader.GetInt32(7),
                GoalDifference = reader.GetInt32(8)
            });
        }

        return standings;
    }
    public List<MatchDetails> GetMatchesForTeam(string teamName)
    {
        List<MatchDetails> matches = new List<MatchDetails>();


        using (var connection = OpenConnection())
        {
            string query = @"
        SELECT
            m.MatchID,
            m.MatchdayID,
            m.HomeTeamName,
            m.AwayTeamName,
            mr.HomeTeamScore,
            mr.AwayTeamScore
        FROM
            Matches m
        JOIN
            MatchResults mr ON m.MatchID = mr.MatchID
        WHERE
            m.HomeTeamName = @teamName OR m.AwayTeamName = @teamName;";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@teamName", teamName);


                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        matches.Add(new MatchDetails
                        {
                            MatchID = reader.GetInt32(0),
                            MatchdayID = reader.GetInt32(1),
                            HomeTeamName = reader.GetString(2),
                            AwayTeamName = reader.GetString(3),
                            HomeTeamScore = reader.GetInt32(4),
                            AwayTeamScore = reader.GetInt32(5)
                        });
                    }
                }
            }
        }
        return matches;
    }

}
