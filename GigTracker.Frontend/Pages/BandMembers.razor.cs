using GigTracker.Frontend.Constants;
using GigTracker.Frontend.Layout;
using GigTracker.Frontend.Services;
using GigTracker.Models;
using Microsoft.AspNetCore.Components;

namespace GigTracker.Frontend.Pages
{
    public partial class BandMembers : ComponentBase
    {
        [CascadingParameter] protected MainLayout? MainLayout { get; set; }

        [Inject] public required BandMemberService BandMemberService { get; set; }
        [Inject] public required DialogService DialogService { get; set; }

        private bool _isLoading = false;
        private List<BandMember> _bandMembers = [];

        private async Task GetDataAsync()
        {
            _isLoading = true;

            try
            {
                _bandMembers = await BandMemberService.GetBandMembersAsync();
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

        private async Task OnDeleteBandAsync(int bandMemberId)
        {
            try
            {
                await BandMemberService.DeleteBandMemberAsync(bandMemberId);
                await GetDataAsync();

                MainLayout?.ShowMessage($"Band Member: {bandMemberId} deleted successfully.", MudBlazor.Severity.Success);
            }
            catch (Exception ex)
            {
                MainLayout?.ShowMessage(ex.Message, MudBlazor.Severity.Error);
            }
        }

        private void OpenCreateBandMemberDialog() => DialogService.OpenBandMemberFormDialog(GetDataAsync);

        private void OpenUpdateBandMemberDialog(BandMember bandMember) => DialogService.OpenBandMemberFormDialog(GetDataAsync, bandMember);

        private void OpenDeleteBandMemberDialog(int bandId) => DialogService.OpenDeleteDialogAsync(GlobalConstants.ENTITY_NAME_BANDMEMBER, bandId, OnDeleteBandAsync);

        protected override async Task OnInitializedAsync() => await GetDataAsync();
    }
}
