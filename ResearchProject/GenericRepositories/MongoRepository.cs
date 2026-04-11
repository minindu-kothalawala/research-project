using MongoDB.Driver;
using ResearchProject.Data;
using ResearchProject.IRepositories;
using ResearchProject.Models;

namespace ResearchProject.Repositories
{
    public class MongoRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly MongoDbContext _context;

        public MongoRepository(MongoDbContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll() =>
            _context.GetCollection<T>().Find(_ => true).ToList();

        public T? GetById(int id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            return _context.GetCollection<T>().Find(filter).FirstOrDefault();
        }

        public void Add(T entity)
        {
            var last = _context.GetCollection<T>()
                .Find(_ => true)
                .Sort(Builders<T>.Sort.Descending("_id"))
                .Limit(1)
                .FirstOrDefault();

            entity.Id = (last?.Id ?? 0) + 1;
            _context.GetCollection<T>().InsertOne(entity);
        }

        public bool Update(T entity)
        {
            var filter = Builders<T>.Filter.Eq("_id", entity.Id);
            var result = _context.GetCollection<T>().ReplaceOne(filter, entity);
            return result.ModifiedCount > 0;
        }

        public bool Delete(int id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            var result = _context.GetCollection<T>().DeleteOne(filter);
            return result.DeletedCount > 0;
        }
    }
}
