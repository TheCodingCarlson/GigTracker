using GigTracker.Frontend.Constants;
using GigTracker.Frontend.Layout;
using GigTracker.Frontend.Services;
using GigTracker.Models;
using Microsoft.AspNetCore.Components;

namespace GigTracker.Frontend.Pages
{
    public partial class Bands : ComponentBase
    {
        [CascadingParameter] protected MainLayout? MainLayout { get; set; }

        [Inject] public required BandService BandService { get; set; }
        [Inject] public required DialogService DialogService { get; set; }

        private bool _isLoading = false;
        private List<Band> _bands = [];

        private async Task GetDataAsync()
        {
            _isLoading = true;

            try
            {
                _bands = await BandService.GetBandsAsync();
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

        private async Task OnDeleteBandAsync(int bandId)
        {
            try
            {
                await BandService.DeleteBandAsync(bandId);
                await GetDataAsync();

                MainLayout?.ShowMessage($"Band: {bandId} deleted successfully.", MudBlazor.Severity.Success);
            }
            catch (Exception ex)
            {
                MainLayout?.ShowMessage(ex.Message, MudBlazor.Severity.Error);
            }
        }

        private void OpenCreateBandDialog() => DialogService.OpenBandFormDialog(GetDataAsync);

        private void OpenUpdateBandDialog(Band band) => DialogService.OpenBandFormDialog(GetDataAsync, band);

        private void OpenDeleteBandDialog(int bandId) => DialogService.OpenDeleteDialogAsync(GlobalConstants.ENTITY_NAME_BAND, bandId, OnDeleteBandAsync);

        protected override async Task OnInitializedAsync() => await GetDataAsync();
    }
}
