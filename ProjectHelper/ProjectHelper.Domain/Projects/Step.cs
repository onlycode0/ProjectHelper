namespace ProjectHelper.Domain.Projects
{
    public class Step
    {
        public string Id { get; set; }

        public List<string> TaskIds { get; set; } = new List<string>();
    }
}
