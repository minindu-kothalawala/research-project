using Microsoft.Extensions.Options;
using ResearchProject.Configuration;
using ResearchProject.Repositories;

namespace ResearchProject.Factories
{
    public class ProductRepositoryFactory : IProductRepositoryFactory
    {
        private readonly IServiceProvider _provider;
        private readonly DatabaseSettings _settings;

        public ProductRepositoryFactory(IServiceProvider provider, IOptions<DatabaseSettings> settings)
        {
            _provider = provider;
            _settings = settings.Value;
        }

        public IProductRepository Create()
        {
            return _settings.DefaultDb switch
            {
                "PostgreSql" => _provider.GetRequiredService<SqlProductRepository>(),
                "MongoDb"    => _provider.GetRequiredService<MongoProductRepository>(),
                _            => throw new InvalidOperationException(
                                    $"Unsupported DefaultDb value: '{_settings.DefaultDb}'. " +
                                    $"Valid values are 'PostgreSql' or 'MongoDb'.")
            };
        }
    }
}
