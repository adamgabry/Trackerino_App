using CommunityToolkit.Mvvm.Input;
using Trackerino.App.Services;
using Trackerino.BL.Facades;
using Trackerino.BL.Models;

namespace Trackerino.App.ViewModels
{
    public partial class UserLoginViewModel : ViewModelBase
    {
        private readonly IUserFacade _userFacade;
        private readonly INavigationService _navigationService;

        public IEnumerable<UserListModel> Users { get; set; } = null!;

        public UserLoginViewModel(
            IUserFacade userFacade,
            INavigationService navigationService,
            IMessengerService messengerService)
            : base(messengerService)
        {
            _userFacade = userFacade;
            _navigationService = navigationService;
        }

        protected override async Task LoadDataAsync()
        {
            await base.LoadDataAsync();
            Preferences.Set("ActiveUser", Guid.Empty.ToString());
            Users = await _userFacade.GetAsync();
        }

        [RelayCommand]
        private async Task LoginAsync(Guid id)
        {
            Preferences.Set("ActiveUser", id.ToString());
            await _navigationService.GoToAsync("//activities");
        }
    }
}