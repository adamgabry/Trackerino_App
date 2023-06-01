using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Trackerino.App.Messages;
using Trackerino.App.Services.Interfaces;
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
        public ICommand FilterLastWeekActivitiesCommand { get; }
        public ICommand FilterLastMonthActivitiesCommand { get; }
        public ICommand FilterPreviousMonthActivitiesCommand { get; }
        public ICommand FilterPreviousYearActivitiesCommand { get; }
        public ICommand ResetFilterActivitiesCommand { get; }
        public ActivityListViewModel(
            IActivityFacade activityFacade,
            INavigationService navigationService,
            IMessengerService messengerService)
            : base(messengerService)
        {
            _activityFacade = activityFacade;
            _navigationService = navigationService;

            StartDateTime = DateTime.Now.AddDays(-7);

            EndDateTime = DateTime.Now;

            FilterActivitiesCommand = new AsyncRelayCommand(FilterActivitiesAsync);

            FilterLastWeekActivitiesCommand = new AsyncRelayCommand(FilterLastWeekActivitiesAsync);

            FilterPreviousMonthActivitiesCommand = new AsyncRelayCommand(FilterPreviousMonthActivitiesAsync);

            FilterLastMonthActivitiesCommand = new AsyncRelayCommand(FilterLastMonthActivitiesAsync);

            FilterPreviousYearActivitiesCommand = new AsyncRelayCommand(FilterPreviousYearActivitiesAsync);

            ResetFilterActivitiesCommand = new AsyncRelayCommand(ResetFilterActivitiesAsync);
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

        public Task UpdateDatePicker(DateTime startDate, DateTime endDate )
        {
            StartDateTime = startDate;
            EndDateTime = endDate;
            return Task.CompletedTask;
        }
        public async Task FilterLastWeekActivitiesAsync()
        {
            DateTime startDate = DateTime.Now.AddDays(-7);
            DateTime endDate = DateTime.Now;
            Activities = await _activityFacade.GetFilteredAsync(startDate, endDate);
            await UpdateDatePicker(startDate, endDate);
        }
        public async Task FilterLastMonthActivitiesAsync()
        {
            DateTime startDate = DateTime.Now.AddMonths(-1);
            DateTime endDate = DateTime.Now;
            Activities = await _activityFacade.GetFilteredAsync(startDate, endDate);
            await UpdateDatePicker(startDate, endDate);
        }
        public async Task FilterPreviousMonthActivitiesAsync()
        {
            var pMonth = DateTime.Today.AddMonths(-1);
            var startDate = new DateTime(pMonth.Year, pMonth.Month, 1);
            var endDate = new DateTime(pMonth.Year, pMonth.Month, DateTime.DaysInMonth(pMonth.Year, pMonth.Month));

            Activities = await _activityFacade.GetFilteredAsync(startDate, endDate);
            await UpdateDatePicker(startDate, endDate);
        }
        public async Task FilterPreviousYearActivitiesAsync()
        {
            var today = DateTime.Today;
            var startDate = new DateTime(today.Year, 1, 1).AddYears(-1);
            var endDate = new DateTime(today.Year, 12, 31).AddYears(-1);

            Activities = await _activityFacade.GetFilteredAsync(startDate, endDate);
            await UpdateDatePicker(startDate, endDate);
        }
        public async Task ResetFilterActivitiesAsync()
        {
            Activities = await _activityFacade.GetAsync();
            
            
            await UpdateDatePicker(DateTime.Today, DateTime.Today);
            await base.LoadDataAsync();
        }
    }
}
