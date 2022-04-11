using Layouts.Shared;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Layouts
{
    public interface ILayoutSelector
    {
        Type SelectMainLayout();
    }

    public class LayoutSelector : ILayoutSelector
    {
        private readonly LayoutConfig config;
        private readonly IStorage storage;

        public LayoutSelector(IOptions<LayoutConfig> config, IStorage storage)
        {
            this.config = config.Value;
            this.storage = storage;
        }

        public Type SelectMainLayout()
        {

            // Simulo una chiamata API che mi setta il colore del cliente
            storage.MainColor = "Orange";
            //


            return Type.GetType(config.LayoutName)!;
        }
    }

    public class LayoutConfig
    {
        public string LayoutName { get; set; } = null!;
    }
}
