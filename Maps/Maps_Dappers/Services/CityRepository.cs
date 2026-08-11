using Dapper;
namespace Maps_Dappers.Services
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetAllAsync(CancellationToken ct = default);
    }

    public class CityRepository : ICityRepository
    {
        private readonly IDbConnectionFactory _factory;

        public CityRepository(IDbConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<City>> GetAllAsync(CancellationToken ct = default)
        {
            using var conn = _factory.CreateConnection();
            var sql = "SELECT Id, Name, SnapChartValues, Latitude, Longitude FROM Cities ORDER BY Name;";
            return await conn.QueryAsync<City>(sql);
        }
    }
}
