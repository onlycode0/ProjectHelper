namespace ProjectHelper.Data
{
    public class MongoDBSettingsModel
    {
        public string ConnectionURI { get; set; } = null!;

        public string DataBaseName { get; set; } = null!;

        public string ProductManagersCollection { get; set; } = null!;

        public string DevelopersCollection { get; set; } = null!;

    }
}
