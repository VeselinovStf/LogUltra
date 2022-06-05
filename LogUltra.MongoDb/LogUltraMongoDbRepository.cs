using LogUltra.Core.Abstraction;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq;

namespace LogUltra.MongoDb
{
    public class LogUltraMongoDbRepository : ILogUltraRepository<LogUltra.Models.Log>
    {
        private readonly IMongoQueryable<LogUltra.Models.Log> _books;

        public LogUltraMongoDbRepository(ILogUltraDataSetting settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _books = database
                .GetCollection<LogUltra.Models.Log>(settings.LogCollectionName)
                .AsQueryable();
        }

        public IQueryable<LogUltra.Models.Log> GetAll()
        {
            return this._books;
        }
    }
}
