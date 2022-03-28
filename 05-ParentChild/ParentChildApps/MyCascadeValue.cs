using System.Diagnostics;

namespace ParentChildApps
{
    [DebuggerDisplay("Color = {ColorValue}")]
    public class MyCascadeValue
    {
        public string ColorValue { get; set; } = "Grey";
    }
}
