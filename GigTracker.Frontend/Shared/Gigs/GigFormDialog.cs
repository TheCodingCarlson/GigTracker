using GigTracker.Frontend.Layout;
using GigTracker.Frontend.Services;
using GigTracker.Frontend.Utils;
using GigTracker.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GigTracker.Frontend.Shared.Gigs
{
    public partial class GigFormDialog : ComponentBase
    {
        [CascadingParameter] protected MainLayout? MainLayout { get; set; }
        [CascadingParameter] private IMudDialogInstance? MudDialog { get; set; }

        [Inject] public required GigService GigService { get; set; }
        [Inject] public required BandService BandService { get; set; }

        [Parameter]
        public Gig? ExistingGig { get; set; }

        [Parameter]
        public Func<Task>? OnGetDataAsync { get; set; }

        private MudForm? _form;
        private bool _isFormValid = false;
        private string[] _formErrors = [];

        private bool _isLoading = false;
        private Gig _newGig = new();
        private List<Band> _bands = [];

        private void OnClose() => MudDialog?.Cancel();

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

                if (ExistingGig is not null)
                {
                    await GigService.UpdateGigAsync(_newGig);
                    successMessage = $"Gig: {ExistingGig.Id} successfully updated";
                }
                else
                {
                    await GigService.CreateGigAsync(_newGig);
                    successMessage = "Gig successfully created";
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
            if (ExistingGig is not null)
            {
                _newGig = ObjectUtils.DeepClone(ExistingGig);
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
