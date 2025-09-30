using MudBlazor;

namespace GigTracker.Frontend.Constants
{
    public static class DialogConstants
    {
        public static readonly DialogOptions DefaultDialogOptions = new()
        {
            BackdropClick = false,
            FullWidth = true,
            MaxWidth = MaxWidth.Large,
            CloseOnEscapeKey = true
        };
    }
}
