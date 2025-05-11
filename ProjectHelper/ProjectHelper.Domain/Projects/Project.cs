namespace ProjectHelper.Domain.Projects
{
    public class Project
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime DeadLine { get; set; }

        public Priority Priority { get; set; }

        public Status Status { get; set; }

        public List<string> DeveloperIds { get; set; } =  new List<string>();

        public List<string> StageIds { get; set; } = new List<string>();
    }
}
