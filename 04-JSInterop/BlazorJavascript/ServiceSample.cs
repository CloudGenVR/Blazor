using Microsoft.JSInterop;

namespace BlazorJavascript
{
    public class ServiceSample
    {
        public string ValoreDaCambiare { get; set; } = "FromService";

        [JSInvokable]
        public async Task CambiaValoreAllaUI(string nuovoValore)
        {
            ValoreDaCambiare = nuovoValore;
        }
    }
}
