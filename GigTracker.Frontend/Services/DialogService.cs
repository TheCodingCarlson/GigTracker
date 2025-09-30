using GigTracker.Frontend.Constants;
using GigTracker.Frontend.Shared;
using GigTracker.Frontend.Shared.Gigs;
using GigTracker.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GigTracker.Frontend.Services
{
    public class DialogService(IDialogService dialogService)
    {
        private readonly IDialogService _dialogService = dialogService;

        public void OpenGigFormDialog(Func<Task>? onGetDataAsync, Gig? existingGig = null)
        {
            var parameters = new DialogParameters<GigFormDialog>
            {
                { x => x.OnGetDataAsync, onGetDataAsync },
                { x => x.ExistingGig, existingGig }
            };

            var title = existingGig is null ? "Create Gig" : "Edit Gig";

            _dialogService.ShowAsync<GigFormDialog>(title, parameters, DialogConstants.DefaultDialogOptions);
        }

        public Task ShowDeleteDialogAsync(string entityName, int entityId, Func<int, Task> onDelete, string? message = null)
        {
            var parameters = new DialogParameters<DeleteConfirmationDialog>
            {
                { x => x.EntityName, entityName },
                { x => x.EntityId, entityId },
                { x => x.Message, message ?? $"Are you sure you want to delete this {entityName}?" },
                { x => x.OnDelete, EventCallback.Factory.Create(this, onDelete) }
            };

            return _dialogService.ShowAsync<DeleteConfirmationDialog>(
                $"Delete {entityName}", parameters, DialogConstants.DefaultDialogOptions);
        }
    }
}
