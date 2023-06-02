using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Trackerino.App.Messages;
using Trackerino.App.Services.Interfaces;
using Trackerino.App.Services;
using Trackerino.BL.Facades.Interfaces;
using Trackerino.BL.Models;

namespace Trackerino.App.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public partial class ActivityDetailViewModel : ViewModelBase, IRecipient<ActivityEditMessage>
    {
        private readonly IActivityFacade _activityFacade;
        private readonly INavigationService _navigationService;

        public Guid Id { get; set; }
        public ActivityDetailModel? Activity { get; private set; }

        public ActivityDetailViewModel(
            IActivityFacade activityFacade,
            INavigationService navigationService,
            IMessengerService messengerService,
            IAlertService alertService)
            : base(messengerService)
        {
            _activityFacade = activityFacade;
            _navigationService = navigationService;
        }

        protected override async Task LoadDataAsync()
        {
            await base.LoadDataAsync();

            Activity = await _activityFacade.GetAsync(Id);
        }

        [RelayCommand]
        private async Task DeleteAsync()
        {
            if (Activity is not null)
            {
                try
                {
                    await _activityFacade.DeleteAsync(Activity.Id);
                    MessengerService.Send(new ActivityDeleteMessage());
                    _navigationService.SendBackButtonPressed();
                }
                catch (InvalidOperationException)
                {
                    //TODO await _alertService.DisplayAsync(ActivityDetailViewModelTexts.DeleteError_Alert_Title, ActivityDetailViewModelTexts.DeleteError_Alert_Message);
                }
            }
        }

        [RelayCommand]
        private async Task GoToEditAsync()
        {
            await _navigationService.GoToAsync("/edit",
                new Dictionary<string, object?> { [nameof(ActivityEditViewModel.Activity)] = Activity });
        }

        public async void Receive(ActivityEditMessage message)
        {
            if (message.ActivityId == Activity?.Id)
            {
                await LoadDataAsync();
            }
        }
    }
}
