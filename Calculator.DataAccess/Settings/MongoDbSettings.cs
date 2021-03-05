using Calculator.DataAccess.Interfaces;

namespace Calculator.DataAccess.Settings
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string DatabaseName { get; set; }

        public string ConnectionString { get; set; }
    }
}
