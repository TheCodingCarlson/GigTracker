using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GigTracker.Frontend.Layout
{
    public partial class MainLayout : LayoutComponentBase
    {
        [Inject] public required ISnackbar MessageStack { get; set; }

        public void ShowMessage(string message, Severity color = Severity.Normal, string position = Defaults.Classes.Position.TopRight)
        {
            MessageStack.Configuration.PositionClass = position;
            MessageStack.Add(message, color,
                options => { options.VisibleStateDuration = 5000; });
        }
    }
}
