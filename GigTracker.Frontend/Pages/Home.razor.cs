using GigTracker.Frontend.Layout;
using GigTracker.Frontend.Services;
using GigTracker.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GigTracker.Frontend.Pages
{
    public partial class Home : ComponentBase
    {
        [CascadingParameter] protected MainLayout? MainLayout { get; set; }

        [Inject] public required GigService GigService { get; set; }

        private record BandGigGroup(string BandName, double Count);

        private List<Gig> _gigs = [];
        private List<BandGigGroup> _bandGigGroups = [];
        private bool _isLoading = false;

        protected override async Task OnInitializedAsync()
        {
            _isLoading = true;

            try
            {
                _gigs = [.. (await GigService.GetGigsAsync()).Where(g => g.Date?.Year == DateTime.Now.Year)];

                // Group gigs by band name and count occurrences
                _bandGigGroups = [.. _gigs
                    .Where(g => g.Band != null)
                    .GroupBy(g => g.Band!.Name)
                    .Select(group => new BandGigGroup(group.Key, group.Count()))
                    .OrderBy(b => b.BandName)];
            }
            catch (Exception ex)
            {
                MainLayout?.ShowMessage(ex.Message, Severity.Error);
            }
            finally
            {
                _isLoading = false;
                StateHasChanged();
            }
        }
    }
}
