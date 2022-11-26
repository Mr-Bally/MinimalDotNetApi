using Dapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.DbAccess;

public class SqlDataAccess : ISqlDataAccess
{
    private const double _cacheTimeToLive = 1d;

    private readonly IConfiguration _configuration;
    private readonly IMemoryCache _memoryCache;

    public SqlDataAccess(IConfiguration configuration, IMemoryCache memoryCache)
    {
        _configuration = configuration;
        _memoryCache = memoryCache;
    }

    public async Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure,
        U parameters,
        string connectionId = "default")
    {
        using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString(connectionId));

        return await connection.QueryAsync<T>(storedProcedure,
            parameters,
            commandType: CommandType.StoredProcedure);
    }

    public async Task SaveData<T>(string storedProcedure,
        T parameters,
        string connectionId = "default")
    {
        using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString(connectionId));

        await connection.ExecuteAsync(storedProcedure,
            parameters,
            commandType: CommandType.StoredProcedure);

        _memoryCache.Remove(nameof(T));
    }

    public async Task<IEnumerable<T>> LoadAllData<T, U>(string storedProcedure,
        string connectionId = "default")
    {
        var cachedData = _memoryCache.Get<IEnumerable<T>>(nameof(T));

        if (cachedData is null)
        {
            using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString(connectionId));

            var results = await connection.QueryAsync<T>(storedProcedure,
                commandType: CommandType.StoredProcedure);

            _memoryCache.Set(nameof(T), results, TimeSpan.FromMinutes(_cacheTimeToLive));

            return results;
        }

        return cachedData;
    }
}
