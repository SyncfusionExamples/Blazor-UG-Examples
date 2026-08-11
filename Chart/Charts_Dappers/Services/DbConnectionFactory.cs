
using System.Data;
using Microsoft.Data.Sqlite;

namespace Charts_Dappers.Services
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
                SnapChartValues INTEGER NULL
            );";
            cmd.ExecuteNonQuery();

            if (needSeed)
            {
                using var insert = conn.CreateCommand();
                insert.CommandText = @"
                INSERT INTO Cities (Name, SnapChartValues) VALUES
                ('United States', 102),
                ('India', 28),
                ('United Kingdom', 18),
                ('Mexico', 16),
                ('Japan', 31),
                ('Brazil', 13),
                ('Germany', 11),
                ('Russia', 8),
                ('Philippines', 8),
                ('Iraq', 7),
                ('Egypt', 7);";
                insert.ExecuteNonQuery();
            }
        }
    }
}
