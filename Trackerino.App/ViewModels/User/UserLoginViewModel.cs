using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;
using Trackerino.App.Messages;
using Trackerino.App.Services;
using Trackerino.App.Services.Interfaces;
using Trackerino.BL.Facades.Interfaces;
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

            Users = await _userFacade.GetAsync();
        }

        [RelayCommand]
        private async Task LoginAsync(Guid id)
        {
            Preferences.Set("ActiveUser", id.ToString());
            await _navigationService.GoToAsync("//activities");
        }

        //[RelayCommand]
        //private async Task GoToDetailAsync(Guid id)
        //{
        //    await _navigationService.GoToAsync<UserDetailViewModel>(
        //        new Dictionary<string, object?> { [nameof(UserDetailViewModel.Id)] = id });
        //}

        //public async void Receive(UserEditMessage message)
        //{
        //    await LoadDataAsync();
        //}

        //public async void Receive(UserDeleteMessage message)
        //{
        //    await LoadDataAsync();
        //}
    }
}