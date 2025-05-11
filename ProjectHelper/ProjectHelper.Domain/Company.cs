namespace ProjectHelper.Domain
{
    public class Company
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public List<string> ProjectIds { get; set; }

        public List<string> DeveloperIds { get; set; }

    }
}
