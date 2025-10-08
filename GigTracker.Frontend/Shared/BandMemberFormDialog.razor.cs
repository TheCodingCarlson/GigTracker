using GigTracker.Frontend.Layout;
using GigTracker.Frontend.Services;
using GigTracker.Frontend.Utils;
using GigTracker.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GigTracker.Frontend.Shared
{
    public partial class BandMemberFormDialog : ComponentBase
    {
        [CascadingParameter] protected MainLayout? MainLayout { get; set; }
        [CascadingParameter] private IMudDialogInstance? MudDialog { get; set; }

        [Inject] public required BandService BandService { get; set; }
        [Inject] public required BandMemberService BandMemberService { get; set; }

        [Parameter]
        public BandMember? ExistingBandMember { get; set; }

        [Parameter]
        public Func<Task>? OnGetDataAsync { get; set; }

        private MudForm? _form;
        private bool _isFormValid = false;
        private string[] _formErrors = [];

        private bool _isLoading = false;
        private BandMember _bandMember = new();
        private List<Band> _bands = [];

        private void OnClose() => MudDialog?.Cancel();

        private void OnInstrumentsChanged(IEnumerable<string> instruments) => _bandMember.Instruments = [.. instruments];

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

                if (ExistingBandMember is not null)
                {
                    await BandMemberService.UpdateBandMemberAsync(_bandMember);
                    successMessage = $"Band Member: {ExistingBandMember.Id} successfully updated";
                }
                else
                {
                    await BandMemberService.CreateBandMemberAsync(_bandMember);
                    successMessage = "Band Member successfully created";
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
            if (ExistingBandMember is not null)
            {
                _bandMember = ObjectUtils.DeepClone(ExistingBandMember);
            }

            _isLoading = true;

            try
            {
                _bands = await BandService.GetBandsAsync();
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
