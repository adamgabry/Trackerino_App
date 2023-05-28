using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Trackerino.App.Messages;
using Trackerino.App.Services.Interfaces;
using Trackerino.App.Services;
using Trackerino.BL.Facades.Interfaces;
using Trackerino.BL.Models;
using System.Windows.Input;

namespace Trackerino.App.ViewModels
{
    public partial class ActivityListViewModel : ViewModelBase, IRecipient<ActivityEditMessage>, IRecipient<ActivityDeleteMessage>
    {
        private readonly IActivityFacade _activityFacade;
        private readonly INavigationService _navigationService;

        public IEnumerable<ActivityListModel> Activities { get; set; } = null!;

        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

        public ICommand FilterActivitiesCommand { get; }

        public ActivityListViewModel(
            IActivityFacade activityFacade,
            INavigationService navigationService,
            IMessengerService messengerService)
            : base(messengerService)
        {
            _activityFacade = activityFacade;
            _navigationService = navigationService;

            FilterActivitiesCommand = new AsyncRelayCommand(FilterActivitiesAsync);
        }

        protected override async Task LoadDataAsync()
        {
            await base.LoadDataAsync();

            Activities = await _activityFacade.GetAsync();
        }

        [RelayCommand]
        private async Task GoToCreateAsync()
        {
            await _navigationService.GoToAsync("/edit");
        }

        [RelayCommand]
        private async Task GoToDetailAsync(Guid id)
        {
            await _navigationService.GoToAsync<ActivityDetailViewModel>(
                new Dictionary<string, object?> { [nameof(ActivityDetailViewModel.Id)] = id });
        }

        public async void Receive(ActivityEditMessage message)
        {
            await LoadDataAsync();
        }

        public async void Receive(ActivityDeleteMessage message)
        {
            await LoadDataAsync();
        }

        public async Task FilterActivitiesAsync()
        {
            // Retrieve the selected start and end dates from the StartDateTime and EndDateTime properties
            DateTime? startDate = StartDateTime;
            DateTime? endDate = EndDateTime;

            if (startDate != null && endDate != null)
            {
                Activities = await _activityFacade.GetFilteredAsync(startDate.Value, endDate.Value);
            }
            else
            {
                Activities = await _activityFacade.GetAsync();
            }
        }
    }
}
