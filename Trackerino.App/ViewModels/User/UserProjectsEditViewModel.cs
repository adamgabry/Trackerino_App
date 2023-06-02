using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Trackerino.App.Messages;
using Trackerino.App.Services;
using Trackerino.BL.Facades;
using Trackerino.BL.Mappers;
using Trackerino.BL.Models;

namespace Trackerino.App.ViewModels
{
    [QueryProperty(nameof(User), nameof(User))]
    public partial class UserProjectsEditViewModel : ViewModelBase
    {
        private readonly IProjectFacade _projectFacade;
        private readonly IUserProjectFacade _userProjectFacade;
        private readonly IUserProjectModelMapper _userProjectModelMapper;

        public UserDetailModel? User { get; set; }

        public ObservableCollection<ProjectListModel> Projects { get; set; } = new();

        public ProjectListModel? ProjectSelected { get; set; }

        public UserProjectDetailModel? UserProjectNew { get; private set; }

        public UserProjectsEditViewModel(
            IProjectFacade projectFacade,
            IUserProjectFacade userProjectFacade,
            IUserProjectModelMapper userProjectModelMapper,
            IMessengerService messengerService)
            : base(messengerService)
        {
            _projectFacade = projectFacade;
            _userProjectFacade = userProjectFacade;
            _userProjectModelMapper = userProjectModelMapper;
        }

        protected override async Task LoadDataAsync()
        {
            await base.LoadDataAsync();

            Projects.Clear();
            var projects = await _projectFacade.GetAsync();
            foreach (var project in projects)
            {
                Projects.Add(project);
                UserProjectNew = GetProjectNew();
            }
        }

        [RelayCommand]
        private async Task AddNewProjectToUserAsync()
        {
            if (UserProjectNew is not null
                && ProjectSelected is not null
                && User is not null)
            {
                _userProjectModelMapper.MapToExistingDetailModel(UserProjectNew, ProjectSelected);

                await _userProjectFacade.SaveAsync(UserProjectNew, User.Id);
                User.Projects.Add(_userProjectModelMapper.MapToListModel(UserProjectNew));

                UserProjectNew = GetProjectNew();

                MessengerService.Send(new UserProjectsAddMessage());
            }
        }

        [RelayCommand]
        private async Task UpdateProjectAsync(UserProjectListModel? model)
        {
            if (model is not null
                && User is not null)
            {
                await _userProjectFacade.SaveAsync(model, User.Id);

                MessengerService.Send(new UserActivitiesEditMessage());
            }
        }

        [RelayCommand]
        private async Task RemoveProjectAsync(UserProjectListModel model)
        {
            if (User is not null)
            {
                await _projectFacade.DeleteAsync(model.Id);
                User.Projects.Remove(model);

                MessengerService.Send(new UserActivitiesDeleteMessage());
            }
        }

        private UserProjectDetailModel GetProjectNew()
        {
            var projectFirst = Projects.First();
            return new()
            {
                Id = Guid.NewGuid(),
                ProjectId = projectFirst.Id,
                ProjectName = projectFirst.Name,
            };
        }
    }
}
