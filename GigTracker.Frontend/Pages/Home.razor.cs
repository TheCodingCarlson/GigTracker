using GigTracker.Frontend.Layout;
using GigTracker.Frontend.Services;
using GigTracker.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Globalization;

namespace GigTracker.Frontend.Pages
{
    public partial class Home : ComponentBase
    {
        [CascadingParameter] protected MainLayout? MainLayout { get; set; }

        [Inject] public required GigService GigService { get; set; }

        private List<Gig> _gigsThisYear = [];
        private List<Band> _bandsThisYear = [];
        private string[] _months = [];
        private List<ChartSeries> _chartData = [];
        private bool _isLoading = false;

        protected override async Task OnInitializedAsync()
        {
            _isLoading = true;

            try
            {
                _gigsThisYear = [.. (await GigService.GetGigsAsync()).Where(g => g.Date?.Year == DateTime.Now.Year)];

                // Get all months present in the gigs, in calendar order
                var monthNames = DateTimeFormatInfo.CurrentInfo!.AbbreviatedMonthNames
                    .Where(m => !string.IsNullOrEmpty(m))
                    .ToArray();

                _months = [.. monthNames.Where(m => _gigsThisYear.Any(g => g.Date?.ToString("MMM") == m))];

                // Extract unique bands from gigs
                _bandsThisYear = _gigsThisYear
                    .Select(g => g.Band!)
                    .DistinctBy(g => g.Name)
                    .ToList();

                // Build a lookup for (band, month) => count
                var gigLookup = _gigsThisYear
                    .Where(g => g.Band != null && g.Date != null)
                    .GroupBy(g => (Band: g.Band!.Name, Month: g.Date?.ToString("MMM")))
                    .ToDictionary(g => g.Key, g => g.Count());

                // Prepare chart data
                _chartData = [.. _bandsThisYear.Select(band =>
                {
                    return new ChartSeries
                    {
                        Name = band?.Name ?? "Unknown",
                        Data = [.. _months.Select(month =>
                        {
                            gigLookup.TryGetValue((band!.Name, month), out var count);
                            return (double)count;
                        })]
                    };
                }).OrderBy(d => d.Name)];
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
