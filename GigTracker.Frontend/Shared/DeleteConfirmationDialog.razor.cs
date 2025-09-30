using GigTracker.Frontend.Constants;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GigTracker.Frontend.Shared
{
    public partial class DeleteConfirmationDialog : ComponentBase
    {
        [CascadingParameter] IMudDialogInstance? MudDialog { get; set; }

        [Parameter]
        public string EntityName { get; set; } = GlobalConstants.ENTITY_NAME_ITEM;

        [Parameter]
        public string? Message { get; set; }

        [Parameter]
        public EventCallback<int> OnDelete { get; set; }

        [Parameter]
        public int EntityId { get; set; }

        private async Task OnConfirm()
        {
            await OnDelete.InvokeAsync(EntityId);
            MudDialog?.Close(DialogResult.Ok(true));
        }

        private void OnCancel() => MudDialog?.Cancel();
    }
}
