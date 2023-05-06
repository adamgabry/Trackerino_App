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
    [QueryProperty(nameof(Id), nameof(Id))]
    public partial class UserDetailViewModel : ViewModelBase, IRecipient<UserEditMessage>, IRecipient<UserActivitiesAddMessage>, IRecipient<UserActivitiesDeleteMessage>,
        IRecipient<UserProjectsAddMessage>, IRecipient<UserProjectsDeleteMessage>
    {
        private readonly IUserFacade userFacade;
        private readonly INavigationService navigationService;
        private readonly IAlertService alertService;

        public Guid Id { get; set; }
        public UserDetailModel? User { get; private set; }

        public UserDetailViewModel(
            IUserFacade userFacade,
            INavigationService navigationService,
            IMessengerService messengerService,
            IAlertService alertService)
            : base(messengerService)
        {
            this.userFacade = userFacade;
            this.navigationService = navigationService;
            this.alertService = alertService;
        }

        protected override async Task LoadDataAsync()
        {
            await base.LoadDataAsync();

            User = await userFacade.GetAsync(Id);
        }

        [RelayCommand]
        private async Task DeleteAsync()
        {
            if (User is not null)
            {
                try
                {
                    await userFacade.DeleteAsync(User.Id);
                    messengerService.Send(new UserDeleteMessage());
                    navigationService.SendBackButtonPressed();
                }
                catch (InvalidOperationException)
                {
                    await alertService.DisplayAsync(UserDetailViewModelTexts.DeleteError_Alert_Title, UserDetailViewModelTexts.DeleteError_Alert_Message);
                }
            }
        }

        [RelayCommand]
        private async Task GoToEditAsync()
        {
            await navigationService.GoToAsync("/edit",
                new Dictionary<string, object?> { [nameof(UserDetailViewModel.User)] = User });
        }

        public async void Receive(UserEditMessage message)
        {
            if (message.UserId == User?.Id)
            {
                await LoadDataAsync();
            }
        }
        public async void Receive(UserActivitiesAddMessage message)
        {
            await LoadDataAsync();
        }

        public async void Receive(UserActivitiesDeleteMessage message)
        {
            await LoadDataAsync();
        }

        public async void Receive(UserProjectsAddMessage message)
        {
            await LoadDataAsync();
        }

        public async void Receive(UserProjectsDeleteMessage message)
        {
            await LoadDataAsync();
        }
    }

}
