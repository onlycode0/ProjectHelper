namespace ProjectHelper.Domain.Projects
{
    public class Stage
    {
        public string Id { get; set; }

        public int InteriorId { get; set; }

        public List<string> StepIds { get; set; } = new List<string>();
    }
}
