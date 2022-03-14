namespace BlazorComponents
{
    public class DashboardConfig
    {
        public List<Panel> Panels { get; set; } = new List<Panel>();
    }

    public class Panel
    {
        public string Title { get; set; }
        public Type Component { get; set; }
    }
}
