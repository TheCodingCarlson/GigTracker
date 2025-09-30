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

        private void OpenCreateGigDialog() => DialogService.OpenGigFormDialog(GetDataAsync);

        protected override async Task OnInitializedAsync() => await GetDataAsync();
    }
}
