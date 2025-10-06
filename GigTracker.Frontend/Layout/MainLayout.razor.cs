using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace GigTracker.Frontend.Layout
{
    public partial class MainLayout : LayoutComponentBase
    {
        [Inject] public required ISnackbar MessageStack { get; set; }
        [Inject] public required IJSRuntime JSRuntime { get; set; }

        private bool _drawerOpen = true;
        private bool _isDarkMode = true;

        private void DrawerToggle() => _drawerOpen = !_drawerOpen;

        public void ShowMessage(string message, Severity color = Severity.Normal, string position = Defaults.Classes.Position.TopRight)
        {
            MessageStack.Configuration.PositionClass = position;
            MessageStack.Add(message, color,
                options => { options.VisibleStateDuration = 5000; });
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender) return;

            var darkMode = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "isDarkMode");
            _isDarkMode = darkMode == "true";

            await JSRuntime.InvokeVoidAsync("document.body.setAttribute", "data-theme", _isDarkMode ? "dark" : "light");

            StateHasChanged();
        }

        private async Task OnDarkModeChanged(bool value)
        {
            _isDarkMode = value;
            await JSRuntime.InvokeVoidAsync("localStorage.setItem", "isDarkMode", value.ToString().ToLower());
            await JSRuntime.InvokeVoidAsync("document.body.setAttribute", "data-theme", _isDarkMode ? "dark" : "light");
        }
    }
}
