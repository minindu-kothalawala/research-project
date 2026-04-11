using Microsoft.Extensions.Options;
using ResearchProject.Configuration;
using ResearchProject.IRepositories;
using ResearchProject.Models;
using ResearchProject.Repositories;

namespace ResearchProject.Factories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly IServiceProvider _provider;
        private readonly DatabaseSettings _settings;

        public RepositoryFactory(IServiceProvider provider, IOptions<DatabaseSettings> settings)
        {
            _provider = provider;
            _settings = settings.Value;
        }

        public IRepository<T> Create<T>() where T : BaseEntity
        {
            return _settings.DefaultDb switch
            {
                "PostgreSql" => _provider.GetRequiredService<SqlRepository<T>>(),
                "MongoDb"    => _provider.GetRequiredService<MongoRepository<T>>(),
                _            => throw new InvalidOperationException(
                                    $"Unsupported DefaultDb value: '{_settings.DefaultDb}'. " +
                                    $"Valid values are 'PostgreSql' or 'MongoDb'.")
            };
        }
    }
}
