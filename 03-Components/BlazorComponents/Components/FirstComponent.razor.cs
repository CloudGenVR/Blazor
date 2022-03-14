using Microsoft.AspNetCore.Components;

namespace BlazorComponents.Components;

public partial class FirstComponent : ComponentBase
{
    [Parameter]
    public string ValueString { get; set; } = null!;

    [Parameter]
    public User Utente { get; set; }

    public int Count { get; set; } = 0;

    private int _nonUtilizzoNellaUI = 0;
    public void Cliccami()
    {
        _nonUtilizzoNellaUI = Count;
        Count++;
    }
}
