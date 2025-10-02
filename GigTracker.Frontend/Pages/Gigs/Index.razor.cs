using GigTracker.Frontend.Layout;
using GigTracker.Frontend.Services;
using GigTracker.Models;
using Microsoft.AspNetCore.Components;

namespace GigTracker.Frontend.Pages.Gigs
{
    public partial class Index : ComponentBase
    {
        [CascadingParameter] protected MainLayout? MainLayout { get; set; }

        [Inject] public required GigService GigService { get; set; }
        [Inject] public required Services.DialogService DialogService { get; set; }

        private bool _isLoading = false;
        private List<Gig> _gigs = [];

        private async Task GetDataAsync()
        {
            _isLoading = true;

            try
            {
                _gigs = await GigService.GetGigsAsync();
            }
            catch (Exception ex)
            {
                MainLayout?.ShowMessage(ex.Message, MudBlazor.Severity.Error);
            }
            finally
            {
                _isLoading = false;
                StateHasChanged();
            }
        }

        private async Task OnDeleteGigAsync(int gigId)
        {
            try
            {
                await GigService.DeleteGigAsync(gigId);
                await GetDataAsync();

                MainLayout?.ShowMessage($"Gig: {gigId} deleted successfully.", MudBlazor.Severity.Success);
            }
            catch (Exception ex)
            {
                MainLayout?.ShowMessage(ex.Message, MudBlazor.Severity.Error);
            }
        }

        private void OpenCreateGigDialog() => DialogService.OpenGigFormDialog(GetDataAsync);

        private void OpenUpdateGigDialog(Gig gig) => DialogService.OpenGigFormDialog(GetDataAsync, gig);

        private void OpenDeleteGigDialog(int gigId) => DialogService.OpenDeleteDialogAsync("Gig", gigId, OnDeleteGigAsync);

        protected override async Task OnInitializedAsync() => await GetDataAsync();
    }
}
