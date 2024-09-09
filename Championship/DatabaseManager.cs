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

        // Elimina la tabella esistente prima di ricrearla, in modo che gli ID ripartano da 1
        string dropTeamsTable = "DROP TABLE IF EXISTS Teams;";
        string dropMatchesTable = "DROP TABLE IF EXISTS Matches;";
        string dropTeamStatsTable = "DROP TABLE IF EXISTS TeamStats;";
        string dropMatchResultsTable = "DROP TABLE IF EXISTS MatchResults;";

        using SQLiteCommand dropCommand1 = new SQLiteCommand(dropTeamsTable, connection);
        dropCommand1.ExecuteNonQuery();

        using SQLiteCommand dropCommand2 = new SQLiteCommand(dropMatchesTable, connection);
        dropCommand2.ExecuteNonQuery();

        using SQLiteCommand dropCommand3 = new SQLiteCommand(dropTeamStatsTable, connection);
        dropCommand3.ExecuteNonQuery();

        using SQLiteCommand dropCommand4 = new SQLiteCommand(dropMatchResultsTable, connection);
        dropCommand4.ExecuteNonQuery();


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
            HomeTeamGoals INTEGER NOT NULL,
            AwayTeamGoals INTEGER NOT NULL,
            MatchDate TEXT NOT NULL,
            FOREIGN KEY (MatchdayID) REFERENCES Matchdays(MatchdayID)
        );";

        string createTeamStatsTable = @"
        CREATE TABLE IF NOT EXISTS TeamStats (
            TeamID INTEGER PRIMARY KEY,
            SquadName TEXT NOT NULL UNIQUE,
            PlayedMatches INTEGER NOT NULL,
            Wins INTEGER NOT NULL,
            Draws INTEGER NOT NULL,
            Losses INTEGER NOT NULL,
            GoalsFor INTEGER NOT NULL,
            GoalsAgainst INTEGER NOT NULL,
            GoalDifference INTEGER NOT NULL,
            Points INTEGER NOT NULL,
            FOREIGN KEY (TeamID) REFERENCES Teams(TeamID)
        );";

        string createMatchResultsTable = @"
        CREATE TABLE IF NOT EXISTS MatchResults (
            MatchID INTEGER PRIMARY KEY AUTOINCREMENT,
            HomeTeamScore INTEGER NOT NULL,
            AwayTeamScore INTEGER NOT NULL,
            FOREIGN KEY (MatchID) REFERENCES Matches(MatchID)
        );";

        // Esegui la creazione delle tabelle
        using SQLiteCommand command1 = new SQLiteCommand(createTeamsTable, connection);
        command1.ExecuteNonQuery();

        using SQLiteCommand command2 = new SQLiteCommand(createMatchesTable, connection);
        command2.ExecuteNonQuery();

        using SQLiteCommand command3 = new SQLiteCommand(createTeamStatsTable, connection);
        command3.ExecuteNonQuery();

        using SQLiteCommand command4 = new SQLiteCommand(createMatchResultsTable, connection);
        command4.ExecuteNonQuery();
    }

    public void InsertData()
    {
        using var connection = OpenConnection();

        string insertTeams = @"
        INSERT INTO Teams (SquadName, FondationYear, City, ColorOfClub, StadiumName) VALUES
        ('Juventus', '1897', 'Turin', 'Black and White', 'Allianz Stadium'),
        ('Milan', '1899', 'Milan', 'Red and Black', 'San Siro'),
        ('Roma', '1927', 'Rome', 'Red and Yellow', 'Stadio Olimpico'),
        ('Napoli', '1926', 'Naples', 'Blue and White', 'Stadio Diego Armando Maradona');";

        string insertMatches = @"
        INSERT INTO Matches (MatchdayID, HomeTeamName, AwayTeamName, HomeTeamGoals, AwayTeamGoals, MatchDate) VALUES
        (1, 'Juventus', 'Napoli', 2, 1, '2024-09-01'),
        (1, 'Milan', 'Roma', 1, 1, '2024-09-01'),
        (2, 'Roma', 'Juventus', 3, 0, '2024-09-08'),
        (2, 'Milan', 'Napoli', 0, 2, '2024-09-08'),
        (3, 'Juventus', 'Milan', 1, 2, '2024-09-15'),
        (3, 'Roma', 'Napoli', 2, 2, '2024-09-15'),
        (4, 'Napoli', 'Juventus', 1, 1, '2024-09-22'),
        (4, 'Roma', 'Milan', 0, 1, '2024-09-22'),
        (5, 'Juventus', 'Roma', 2, 1, '2024-09-29'),
        (5, 'Napoli', 'Milan', 1, 0, '2024-09-29'),
        (6, 'Milan', 'Juventus', 0, 0, '2024-10-06'),
        (6, 'Napoli', 'Roma', 2, 1, '2024-10-06');";

        string insertTeamStats = @"
        INSERT INTO TeamStats (TeamID, SquadName,PlayedMatches, Wins, Draws, Losses, GoalsFor, GoalsAgainst, GoalDifference, Points) VALUES
        (1, 'Juventus', 6, 2, 2, 2, 6, 8, -2, 8),
        (2, 'Milan', 6, 2, 2, 5, 4, 5, -1, 8),
        (3, 'Roma', 6, 1, 2, 3, 8, 8, 0, 5),
        (4, 'Napoli', 6, 3, 2, 1, 9, 6, 3, 11);";

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


        using SQLiteCommand command3 = new SQLiteCommand(insertTeamStats, connection);
        command3.ExecuteNonQuery();


        using SQLiteCommand command4 = new SQLiteCommand(insertMatchResults, connection);
        command4.ExecuteNonQuery();
    }

}
