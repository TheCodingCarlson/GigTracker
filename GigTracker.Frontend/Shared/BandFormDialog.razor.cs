using GigTracker.Frontend.Layout;
using GigTracker.Frontend.Services;
using GigTracker.Frontend.Utils;
using GigTracker.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GigTracker.Frontend.Shared
{
    public partial class BandFormDialog : ComponentBase
    {
        [CascadingParameter] protected MainLayout? MainLayout { get; set; }
        [CascadingParameter] private IMudDialogInstance? MudDialog { get; set; }

        [Inject] public required BandService BandService { get; set; }
        [Inject] public required BandMemberService BandMemberService { get; set; }

        [Parameter]
        public Band? ExistingBand { get; set; }

        [Parameter]
        public Func<Task>? OnGetDataAsync { get; set; }

        private MudForm? _form;
        private bool _isFormValid = false;
        private string[] _formErrors = [];

        private bool _isLoading = false;
        private Band _newBand = new();
        private List<BandMember> _bandMembers = [];

        private void OnClose() => MudDialog?.Cancel();

        private void OnMembersChanged(IEnumerable<BandMember> members) => _newBand.Members = [.. members];

        private void OnGenresChanged(IEnumerable<string> genres) => _newBand.Genres = [.. genres];
        
        private async Task OnSubmit()
        {
            if (_form is not null)
            {
                await _form.Validate();
            }

            if (!_isFormValid) return;

            _isLoading = true;

            try
            {
                string successMessage = string.Empty;

                if (ExistingBand is not null)
                {
                    await BandService.UpdateBandAsync(_newBand);
                    successMessage = $"Band: {ExistingBand.Id} successfully updated";
                }
                else
                {
                    await BandService.CreateBandAsync(_newBand);
                    successMessage = "Band successfully created";
                }

                if (OnGetDataAsync is not null)
                {
                    await OnGetDataAsync.Invoke();
                }

                MainLayout?.ShowMessage(successMessage, Severity.Success);
            }
            catch (Exception ex)
            {
                MainLayout?.ShowMessage(ex.Message, Severity.Error);
            }
            finally
            {
                _isLoading = false;
                OnClose();
            }
        }

        protected override async Task OnInitializedAsync()
        {
            if (ExistingBand is not null)
            {
                _newBand = ObjectUtils.DeepClone(ExistingBand);
            }

            _isLoading = true;

            try
            {
                _bandMembers = await BandMemberService.GetBandMembersAsync();
            }
            catch (Exception ex)
            {
                MainLayout?.ShowMessage(ex.Message, Severity.Error);
            }
            finally
            {
                _isLoading = false;
            }
        }
    }
}
