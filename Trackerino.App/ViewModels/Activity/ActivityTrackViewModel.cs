using CommunityToolkit.Mvvm.Input;
using Trackerino.App.Messages;
using Trackerino.App.Services;
using Trackerino.BL.Facades;
using Trackerino.BL.Models;
using System.Timers;
using Trackerino.DAL.Common;

namespace Trackerino.App.ViewModels
{
    [QueryProperty(nameof(Activity), nameof(Activity))]
    public partial class ActivityTrackViewModel : ViewModelBase
    {
        private readonly IProjectFacade _projectFacade;
        private readonly IActivityFacade _activityFacade;
        private readonly INavigationService _navigationService;
        private readonly IAlertService _alertService;
        private readonly System.Timers.Timer _timer;

        private DateTime _startDateTime = DateTime.UnixEpoch;
        private DateTime _endDateTime = DateTime.UnixEpoch;
        private TimeSpan _duration;

        public List<ActivityTag> ActivityTags { get; set; }

        public IEnumerable<ProjectListModel> Projects { get; set; } = null!;

        public UserDetailModel User { get; set; } = null!;

        public DateTime StartDateTime
        {
            get { return _startDateTime; }
            set
            {
                if (_startDateTime != value)
                {
                    _startDateTime = value;
                    OnPropertyChanged(nameof(StartDateTime));
                }
            }
        }

        public DateTime EndDateTime
        {
            get { return _endDateTime; }
            set
            {
                if (_endDateTime != value)
                {
                    _endDateTime = value;
                    OnPropertyChanged(nameof(EndDateTime));
                }
            }
        } 
        
        public TimeSpan Duration
        {
            get
            {
                return _duration;
            }

            set
            {
                if (_duration != value)
                {
                    _duration = value;
                    OnPropertyChanged(nameof(Duration));
                }
            }
        }

        public ActivityDetailModel Activity { get; init; } = ActivityDetailModel.Empty; 

        public ActivityTrackViewModel(
            IProjectFacade projectFacade,
            IActivityFacade activityFacade,
            IUserFacade userFacade,
            INavigationService navigationService,
            IMessengerService messengerService,
            IAlertService alertService)
            : base(messengerService)
        {
            _projectFacade = projectFacade;
            _activityFacade = activityFacade;
            _navigationService = navigationService;
            _alertService = alertService;

            ActivityTags = new List<ActivityTag>((ActivityTag[])Enum.GetValues(typeof(ActivityTag)));
            Activity.UserId = Guid.Parse(Preferences.Get("ActiveUser", String.Empty));
            Activity.ProjectId = Guid.Empty;
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += TimerElapsed;
            _timer.Start();
        }


        protected override async Task LoadDataAsync()
        {
            await base.LoadDataAsync();
            Projects = await _projectFacade.GetAsync();
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            UpdateDuration();
        }

        private void UpdateDuration()
        {
            TimeSpan duration;

            if (StartDateTime > DateTime.UnixEpoch)
            {
                duration = DateTime.Now - StartDateTime;
            }
            else
            {
                duration = TimeSpan.Zero;
            }

            Duration = duration;
        }

        [RelayCommand]
        private async Task SetStartDateTime()
        {
            if (StartDateTime == DateTime.UnixEpoch)
            {
                StartDateTime = DateTime.Now;
            }
            else
            {
                await _alertService.DisplayAsync("Activity can not be edited", "You cannot start the activity again");
                return;
            }

        }

        [RelayCommand]
        private async Task SetEndDateTime()
        {
            if (StartDateTime != DateTime.UnixEpoch && EndDateTime == DateTime.UnixEpoch)
            {
                _timer.Stop();
                EndDateTime = DateTime.Now;
            }
            else
            {
                await _alertService.DisplayAsync("Activity can not be edited", "You must first start the activity");
                return;
            }

        }

        [RelayCommand]
        private Task ActivityToProjectAsync(Guid projectId)
        {
            Activity.ProjectId = projectId;
            return Task.CompletedTask;
        }

        [RelayCommand]
        private async Task SaveAsync()
        {
            if (StartDateTime == DateTime.UnixEpoch || EndDateTime == DateTime.UnixEpoch)
            {
                await _alertService.DisplayAsync("Activity can not be edited", "You must finish the activity");
                return;
            }
            if(Activity.ProjectId == Guid.Empty)
            {
                await _alertService.DisplayAsync("Activity can not be edited", "You must select a project");
                return;
            }

            Activity.StartDateTime = StartDateTime;
            Activity.EndDateTime = EndDateTime;

            await _activityFacade.SaveAsync(Activity);

            MessengerService.Send(new ActivityEditMessage { ActivityId = Activity.Id });

            _navigationService.SendBackButtonPressed();
        }
    }
}