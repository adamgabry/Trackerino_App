using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Trackerino.App.Messages;
using Trackerino.App.Services.Interfaces;
using Trackerino.App.Services;
using Trackerino.BL.Facades.Interfaces;
using Trackerino.BL.Models;

namespace Trackerino.App.ViewModels
{
    public partial class ProjectListViewModel : ViewModelBase, IRecipient<ProjectEditMessage>, IRecipient<ProjectDeleteMessage>
    {
        private readonly IProjectFacade _projectFacade;
        private readonly INavigationService _navigationService;

        public IEnumerable<ProjectListModel> Projects { get; set; } = null!;

        public ProjectListViewModel(
            IProjectFacade projectFacade,
            INavigationService navigationService,
            IMessengerService messengerService)
            : base(messengerService)
        {
            this._projectFacade = projectFacade;
            this._navigationService = navigationService;
        }

        protected override async Task LoadDataAsync()
        {
            await base.LoadDataAsync();

            Projects = await _projectFacade.GetAsync();
        }

        [RelayCommand]
        private async Task GoToCreateAsync()
        {
            await _navigationService.GoToAsync("/edit");
        }

        [RelayCommand]
        private async Task GoToDetailAsync(Guid id)
        {
            await _navigationService.GoToAsync<ProjectDetailViewModel>(
                new Dictionary<string, object?> { [nameof(ProjectDetailViewModel.Id)] = id });
        }

        public async void Receive(ProjectEditMessage message)
        {
            await LoadDataAsync();
        }

        public async void Receive(ProjectDeleteMessage message)
        {
            await LoadDataAsync();
        }
    }
}
