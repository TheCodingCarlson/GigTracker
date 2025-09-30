using GigTracker.Frontend.Constants;
using GigTracker.Frontend.Shared.Gigs;
using GigTracker.Models;
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
    }
}
