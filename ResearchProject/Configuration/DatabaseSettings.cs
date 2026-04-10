namespace ResearchProject.Configuration
{
    public class DatabaseSettings
    {
        public string DefaultDb { get; set; } = "PostgreSql";
        public PostgreSqlSettings PostgreSql { get; set; } = new();
        public MongoDbSettings MongoDb { get; set; } = new();
    }

    public class PostgreSqlSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
    }

    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
    }
}
