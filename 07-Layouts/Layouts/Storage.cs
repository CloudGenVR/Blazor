namespace Layouts
{
    public class Storage : IStorage
    {
        public string MainColor { get; set; }
    }

    public interface IStorage
    {
        string MainColor { get; set; }
    }
}
