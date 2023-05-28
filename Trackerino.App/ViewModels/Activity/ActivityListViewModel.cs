using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Trackerino.App.Messages;
using Trackerino.App.Services.Interfaces;
using Trackerino.App.Services;
using Trackerino.BL.Facades.Interfaces;
using Trackerino.BL.Models;
using System.Windows.Input;
using Trackerino.BL.Facades;

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
        public ICommand FilterLastWeekActivitiesCommand { get; }
        public ICommand FilterLastMonthActivitiesCommand { get; }
        public ICommand FilterThisMonthActivitiesCommand { get; }
        public ICommand FilterLastYearActivitiesCommand { get; }
        public ActivityListViewModel(
            IActivityFacade activityFacade,
            INavigationService navigationService,
            IMessengerService messengerService)
            : base(messengerService)
        {
            _activityFacade = activityFacade;
            _navigationService = navigationService;

            FilterActivitiesCommand = new AsyncRelayCommand(FilterActivitiesAsync);

            FilterLastWeekActivitiesCommand = new AsyncRelayCommand(FilterLastWeekActivitiesAsync);

            FilterLastMonthActivitiesCommand = new AsyncRelayCommand(FilterLastMonthActivitiesAsync);

            FilterLastMonthActivitiesCommand = new AsyncRelayCommand(FilterThisMonthActivitiesAsync);

            FilterLastYearActivitiesCommand = new AsyncRelayCommand(FilterLastYearActivitiesAsync);
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
        public async Task FilterLastWeekActivitiesAsync()
        {
            DateTime startDate = DateTime.Now.AddDays(-7);
            DateTime endDate = DateTime.Now;
            Activities = await _activityFacade.GetFilteredAsync(startDate, endDate);
        }
        public async Task FilterLastMonthActivitiesAsync()
        {
            DateTime today = DateTime.Today;
            DateTime firstDayOfMonth = new DateTime(today.Year, today.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            DateTime startDate = firstDayOfMonth.AddMonths(-1);
            DateTime endDate = lastDayOfMonth.AddMonths(-1);
            Activities = await _activityFacade.GetFilteredAsync(startDate, endDate);
        }
        public async Task FilterThisMonthActivitiesAsync()
        {
            DateTime today = DateTime.Today;
            DateTime firstDayOfMonth = new DateTime(today.Year, today.Month, 1);

            DateTime startDate = firstDayOfMonth;
            DateTime endDate = today;

            Activities = await _activityFacade.GetFilteredAsync(startDate, endDate);
        }
        public async Task FilterLastYearActivitiesAsync()
        {
            DateTime startDate = DateTime.Now.AddYears(-1);
            DateTime endDate = DateTime.Now;
            Activities = await _activityFacade.GetFilteredAsync(startDate, endDate);
        }
    }
}
