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

namespace Trackerino.App.ViewModels
{
    [QueryProperty(nameof(Activity), nameof(Activity))]
    public partial class ActivityTrackViewModel : ViewModelBase
    {
        private readonly IUserFacade _userFacade;
        private readonly IActivityFacade _activityFacade;
        private readonly INavigationService _navigationService;
        private readonly System.Timers.Timer _timer;

        private DateTime _startDateTime = DateTime.UnixEpoch;
        private DateTime _endDateTime = DateTime.UnixEpoch;
        private TimeSpan _duration;

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
            IActivityFacade activityFacade,
            INavigationService navigationService,
            IMessengerService messengerService)
            : base(messengerService)
        {
            _activityFacade = activityFacade;
            _navigationService = navigationService;

            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += TimerElapsed;
            _timer.Start();
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
        private async Task SaveAsync()
        {
            await _activityFacade.SaveAsync(Activity);

            MessengerService.Send(new ActivityEditMessage { ActivityId = Activity.Id });

            _navigationService.SendBackButtonPressed();
        }
    }
}