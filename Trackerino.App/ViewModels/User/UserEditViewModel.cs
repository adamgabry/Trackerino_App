using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackerino.App.Messages;
using Trackerino.App.Services;
using Trackerino.App.Services.Interfaces;
using Trackerino.BL.Facades.Interfaces;
using Trackerino.BL.Models;

namespace Trackerino.App.ViewModels
{
    [QueryProperty(nameof(User), nameof(User))]
    public partial class UserEditViewModel : ViewModelBase, IRecipient<UserActivitiesEditMessage>, IRecipient<UserActivitiesAddMessage>, IRecipient<UserActivitiesDeleteMessage>,
        IRecipient<UserProjectsAddMessage>, IRecipient<UserProjectsEditMessage>, IRecipient<UserProjectsDeleteMessage>
    {
        private readonly IUserFacade _userFacade;
        private readonly INavigationService _navigationService;

        public UserDetailModel User { get; set; } = UserDetailModel.Empty;

        public UserEditViewModel(
            IUserFacade userFacade,
            INavigationService navigationService,
            IMessengerService messengerService)
            : base(messengerService)
        {
            _userFacade = userFacade;
            _navigationService = navigationService;
        }

        [RelayCommand]
        private async Task GoToUserActivitiesEditAsync()
        {
            await _navigationService.GoToAsync("/activities",
                new Dictionary<string, object?> { [nameof(UserActivitiesEditViewModel.User)] = User });
        }

        [RelayCommand]
        private async Task GoToUserProjectsEditAsync()
        {
            await _navigationService.GoToAsync("/projects",
                new Dictionary<string, object?> { [nameof(UserProjectsEditViewModel.User)] = User });
        }

        [RelayCommand]
        private async Task SaveAsync()
        {
            await _userFacade.SaveAsync(User with { Projects = default!});

            MessengerService.Send(new UserEditMessage { UserId = User.Id });

            _navigationService.SendBackButtonPressed();
        }

        public async void Receive(UserActivitiesEditMessage message)
        {
            await ReloadDataAsync();
        }

        public async void Receive(UserActivitiesAddMessage message)
        {
            await ReloadDataAsync();
        }

        public async void Receive(UserActivitiesDeleteMessage message)
        {
            await ReloadDataAsync();
        }

        public async void Receive(UserProjectsEditMessage message)
        {
            await ReloadDataAsync();
        }

        public async void Receive(UserProjectsAddMessage message)
        {
            await ReloadDataAsync();
        }

        public async void Receive(UserProjectsDeleteMessage message)
        {
            await ReloadDataAsync();
        }

        private async Task ReloadDataAsync()
        {
            User = await _userFacade.GetAsync(User.Id)
                     ?? UserDetailModel.Empty;
        }
    }
}
