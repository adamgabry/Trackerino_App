using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using Trackerino.App.Messages;
using Trackerino.App.Services.Interfaces;
using Trackerino.BL.Facades;
using Trackerino.BL.Facades.Interfaces;
using Trackerino.BL.Models;
using Trackerino.DAL.Repositories;

namespace Trackerino.App.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public partial class ProjectDetailViewModel : ViewModelBase, IRecipient<ProjectEditMessage>, IRecipient<ProjectActivitiesAddMessage>, IRecipient<ProjectActivitiesDeleteMessage>,
        IRecipient<ProjectUsersAddMessage>, IRecipient<ProjectUsersDeleteMessage>
    {

        private readonly IProjectFacade _projectFacade;
        private readonly IProjectUserFacade _projectUserFacade;
        private readonly INavigationService _navigationService;
        private readonly IAlertService _alertService;

        public Guid Id { get; set; }
        public ProjectDetailModel? Project { get; private set; }

        public ProjectDetailViewModel(
            IProjectFacade projectFacade,
            IProjectUserFacade projectUserFacade,
            INavigationService navigationService,
            IMessengerService messengerService,
            IAlertService alertService)
            : base(messengerService)
        {
            _projectFacade = projectFacade;
            _navigationService = navigationService;
            _alertService = alertService;
            _projectUserFacade = projectUserFacade;
        }
        protected override async Task LoadDataAsync()
        {
            await base.LoadDataAsync();

            Project = await _projectFacade.GetAsync(Id);
        }
        [RelayCommand]
        private async Task DeleteAsync()
        {
            if (Project is not null)
            {
                try
                {
                    await _projectFacade.DeleteAsync(Project.Id);
                    MessengerService.Send(new ProjectDeleteMessage());
                    _navigationService.SendBackButtonPressed();
                }
                catch (InvalidOperationException)
                {
                    //await _alertService.DisplayAsync(UserDetailViewModelTexts.DeleteError_Alert_Title, UserDetailViewModelTexts.DeleteError_Alert_Message);
                }
            }
        }

        [RelayCommand]
        private async Task GoToEditAsync()
        {
            await _navigationService.GoToAsync("/edit",
                new Dictionary<string, object?> { [nameof(Project)] = Project });
        }

        private IAsyncRelayCommand _userJoinCommand;
        public IAsyncRelayCommand UserJoinsCommand => _userJoinCommand ??= new AsyncRelayCommand(UserJoinsAsync);

        private async Task UserJoinsAsync()
        {
            await _navigationService.GoToAsync("/edit/users", new Dictionary<string, object?>
            {
                [nameof(ProjectUsersEditViewModel.Project)] = Project
            });
        }

        public async void Receive(ProjectEditMessage message)
        {
            if (message.ProjectId == Project?.Id)
            {
                await LoadDataAsync();
            }
        }
        public async void Receive(ProjectActivitiesAddMessage message)
        {
            await LoadDataAsync();
        }

        public async void Receive(ProjectActivitiesDeleteMessage message)
        {
            await LoadDataAsync();
        }

        public async void Receive(ProjectUsersAddMessage message)
        {
            await LoadDataAsync();
        }

        public async void Receive(ProjectUsersDeleteMessage message)
        {
            await LoadDataAsync();
        }
    }
}
