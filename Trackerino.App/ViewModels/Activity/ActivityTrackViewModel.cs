using CommunityToolkit.Mvvm.Input;
using Trackerino.App.Messages;
using Trackerino.App.Services.Interfaces;
using Trackerino.App.Services;
using Trackerino.BL.Facades.Interfaces;
using Trackerino.BL.Models;
using System.Timers;
using Trackerino.BL.Facades;
using System;
using System.Threading.Tasks;
using System.Timers;
using Trackerino.App.Views.Activity;
using Trackerino.DAL.Common;

namespace Trackerino.App.ViewModels
{
    [QueryProperty(nameof(Activity), nameof(Activity))]
    public partial class ActivityTrackViewModel : ViewModelBase
    {
        private readonly IProjectFacade _projectFacade;
        private readonly IUserFacade _userFacade;
        private readonly IActivityFacade _activityFacade;
        private readonly INavigationService _navigationService;
        private readonly System.Timers.Timer _timer;

        private DateTime _startDateTime = DateTime.UnixEpoch;
        private DateTime _endDateTime = DateTime.UnixEpoch;
        private TimeSpan _duration;

        public List<ActivityTag> ActivityTags { get; set; }

        public IEnumerable<ProjectListModel> Projects { get; set; } = null!;

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
            IMessengerService messengerService)
            : base(messengerService)
        {
            _projectFacade = projectFacade;
            _activityFacade = activityFacade;
            _userFacade = userFacade;
            _navigationService = navigationService;

            ActivityTags = new List<ActivityTag>((ActivityTag[])Enum.GetValues(typeof(ActivityTag)));
            string userIdString = Preferences.Get("ActiveUser", String.Empty);
            Activity.UserId = Guid.Parse(userIdString);
            Activity.ProjectId = Guid.Parse("0D7D53AE-D631-4DAA-8C71-C3370E69A16B");
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
        }

        [RelayCommand]
        private async Task SetEndDateTime()
        {
            if (EndDateTime == DateTime.UnixEpoch)
            {
                _timer.Stop();
                EndDateTime = DateTime.Now;
            }
        }

        [RelayCommand]
        private async Task ActivityToProjectAsync(Guid projectId)
        {
            Activity.ProjectId = projectId;
        }

        [RelayCommand]
        private async Task SaveAsync()
        {
            if (StartDateTime == DateTime.UnixEpoch || EndDateTime == DateTime.UnixEpoch)
            {
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