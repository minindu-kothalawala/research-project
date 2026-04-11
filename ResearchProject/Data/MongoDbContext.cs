using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using ResearchProject.Configuration;
using ResearchProject.Models;

namespace ResearchProject.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        static MongoDbContext()
        {
            var conventions = new ConventionPack { new IgnoreExtraElementsConvention(true) };
            ConventionRegistry.Register("AppConventions", conventions, _ => true);
        }

        public MongoDbContext(IOptions<DatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.MongoDb.ConnectionString);
            _database = client.GetDatabase(settings.Value.MongoDb.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>() where T : BaseEntity =>
            _database.GetCollection<T>(typeof(T).Name);
    }
}

