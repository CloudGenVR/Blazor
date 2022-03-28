namespace ParentChildApps
{
    public class Store : IStore
    {
        public string MyValue { get; set; } = "Violet";
    }

    public interface IStore
    {
        string MyValue { get; set; }
    }
}
