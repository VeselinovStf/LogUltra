using LogUltra.Core.Abstraction;
using LogUltra.Models;
using MongoDB.Driver;
using System.Collections.Generic;

namespace LogUltra.UI.Services
{
    public class LogService
    {
        private readonly IMongoCollection<Log> _books;

        public LogService(ILogUltraDataSetting settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _books = database.GetCollection<Log>(settings.LogCollectionName);
        }

        public List<Log> Get() =>
            _books.Find(book => true).ToList();

        public Log GetById(string id) =>
            _books.Find<Log>(log => log.Id == id).FirstOrDefault();

        public Log GetBySource(string source) =>
            _books.Find<Log>(log => log.Source == source).FirstOrDefault();

        public void Remove(Log logModel) =>
            _books.DeleteOne(log => log.Id == logModel.Id);

        public void Remove(string id) =>
            _books.DeleteOne(log => log.Id == id);
    }
}
