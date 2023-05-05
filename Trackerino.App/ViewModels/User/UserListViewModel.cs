using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using Trackerino.App.Messages;
using Trackerino.App.Services;
using Trackerino.App.Services.Interfaces;
using Trackerino.BL.Facades.Interfaces;
using Trackerino.BL.Models;

namespace Trackerino.App.ViewModels
{
    public partial class UserListViewModel : ViewModelBase, IRecipient<UserEditMessage>, IRecipient<UserDeleteMessage>
    {
        private readonly IUserFacade userFacade;
        private readonly INavigationService navigationService;

        public IEnumerable<UserListModel> Users { get; set; } = null!;

        public UserListViewModel(
            IUserFacade userFacade,
            INavigationService navigationService,
            IMessengerService messengerService)
            : base(messengerService)
        {
            this.userFacade = userFacade;
            this.navigationService = navigationService;
        }

        protected override async Task LoadDataAsync()
        {
            await base.LoadDataAsync();

            Users = await userFacade.GetAsync();
        }

        [RelayCommand]
        private async Task GoToCreateAsync()
        {
            await navigationService.GoToAsync("/edit");
        }

        [RelayCommand]
        private async Task GoToDetailAsync(Guid id)
        {
            await navigationService.GoToAsync<UserDetailViewModel>(
                new Dictionary<string, object?> { [nameof(UserDetailViewModel.Id)] = id });
        }

        public async void Receive(UserEditMessage message)
        {
            await LoadDataAsync();
        }

        public async void Receive(UserDeleteMessage message)
        {
            await LoadDataAsync();
        }
    }
}
