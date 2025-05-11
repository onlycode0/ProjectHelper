namespace ProjectHelper.Data
{
    public class MongoDBSettingsModel
    {
        public string ConnectionURI { get; set; } = null!;

        public string DataBaseName { get; set; } = null!;

        public string ProductManagersCollection { get; set; } = null!;

        public string DevelopersCollection { get; set; } = null!;

        public string CompaniesCollection { get; set; } = null!;

        public string ProjectsCollection { get; set; } = null!;

        public string StagesCollection { get; set; } = null!;

        public string StepsCollection { get; set; } = null!;

        public string ProjectTasksCollection { get; set; } = null!;

    }
}
