
using System.Data;
using Microsoft.Data.Sqlite;

namespace Maps_Dappers.Services
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
        string DbPath { get; }
    }

    public class SqliteConnectionFactory : IDbConnectionFactory
    {
        public string DbPath { get; }

        public SqliteConnectionFactory(IWebHostEnvironment env)
        {
            // Put the SQLite file in the app content root: ./Data/cities.db
            DbPath = Path.Combine(env.ContentRootPath, "Data", "cities.db");
            Directory.CreateDirectory(Path.GetDirectoryName(DbPath)!);
            EnsureDatabase();
        }

        public IDbConnection CreateConnection()
            => new SqliteConnection($"Data Source={DbPath}");

        private void EnsureDatabase()
        {
            var needSeed = !File.Exists(DbPath);
            using var conn = new SqliteConnection($"Data Source={DbPath}");
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
            CREATE TABLE IF NOT EXISTS Cities(
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                SnapChartValues INTEGER NULL,
                Latitude REAL NOT NULL,
                Longitude REAL NOT NULL
            );";
            cmd.ExecuteNonQuery();

            if (needSeed)
            {
                using var insert = conn.CreateCommand();
                insert.CommandText = @"
                INSERT INTO Cities (Name, SnapChartValues, Latitude, Longitude) VALUES
                ('United States', 102, 38.9072, -77.0369),
                ('India', 28, 28.6139, 77.2090),
                ('United Kingdom', 18, 51.5074, -0.1278),
                ('Mexico', 16, 19.4326, -99.1332),
                ('Japan', 31, 35.6762, 139.6503),
                ('Mexico', 26, 19.4326, -99.1332),
                ('Brazil', 13, -15.7939, -47.8828),
                ('Germany', 11, 52.5200, 13.4050),
                ('Russia', 8, 55.7558, 37.6173),
                ('Philippines', 8, 14.5995, 120.9842),
                ('Iraq', 7, 33.3152, 44.3661),
                ('Egypt', 7, 30.0444, 31.2357);";
                insert.ExecuteNonQuery();
            }
        }
    }
}
