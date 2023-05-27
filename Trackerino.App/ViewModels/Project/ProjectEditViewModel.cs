using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Trackerino.App.Messages;
using Trackerino.App.Services.Interfaces;
using Trackerino.App.Services;
using Trackerino.BL.Facades.Interfaces;
using Trackerino.BL.Models;

namespace Trackerino.App.ViewModels
{
    [QueryProperty(nameof(Project), nameof(Project))]
    public partial class ProjectEditViewModel : ViewModelBase, IRecipient<ProjectActivitiesEditMessage>, IRecipient<ProjectActivitiesAddMessage>, IRecipient<ProjectActivitiesDeleteMessage>,
        IRecipient<ProjectUsersAddMessage>, IRecipient<ProjectUsersEditMessage>, IRecipient<ProjectUsersDeleteMessage>
    {
        private readonly IProjectFacade _projectFacade;
        private readonly INavigationService _navigationService;

        public ProjectDetailModel Project { get; set; } = ProjectDetailModel.Empty;

        public ProjectEditViewModel(
            IProjectFacade projectFacade,
            INavigationService navigationService,
            IMessengerService messengerService)
            : base(messengerService)
        {
            _projectFacade = projectFacade;
            _navigationService = navigationService;
        }

        [RelayCommand]
        private async Task GoToProjectActivitiesEditAsync()
        {
            await _navigationService.GoToAsync("/activities",
                new Dictionary<string, object?> { [nameof(ProjectActivitiesEditViewModel.Project)] = Project });
        }

        [RelayCommand]
        private async Task GoToProjectUsersEditAsync()
        {
            await _navigationService.GoToAsync("/users",
                new Dictionary<string, object?> { [nameof(ProjectActivitiesEditViewModel.Project)] = Project });
        }

        [RelayCommand]
        private async Task SaveAsync()
        {
            await _projectFacade.SaveAsync(Project with { Activities = default! , Users = default!});

            MessengerService.Send(new ProjectEditMessage { ProjectId = Project.Id });

            _navigationService.SendBackButtonPressed();
        }

        public async void Receive(ProjectActivitiesEditMessage message)
        {
            await ReloadDataAsync();
        }

        public async void Receive(ProjectActivitiesAddMessage message)
        {
            await ReloadDataAsync();
        }

        public async void Receive(ProjectActivitiesDeleteMessage message)
        {
            await ReloadDataAsync();
        }

        public async void Receive(ProjectUsersEditMessage message)
        {
            await ReloadDataAsync();
        }

        public async void Receive(ProjectUsersAddMessage message)
        {
            await ReloadDataAsync();
        }

        public async void Receive(ProjectUsersDeleteMessage message)
        {
            await ReloadDataAsync();
        }

        private async Task ReloadDataAsync()
        {
            Project = await _projectFacade.GetAsync(Project.Id)
                     ?? ProjectDetailModel.Empty;
        }
    }
}
