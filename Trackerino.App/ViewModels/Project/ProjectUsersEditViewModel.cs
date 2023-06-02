using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using Trackerino.App.Messages;
using Trackerino.App.Services;
using Trackerino.BL.Facades;
using Trackerino.BL.Mappers;
using Trackerino.BL.Models;

namespace Trackerino.App.ViewModels
{
    [QueryProperty(nameof(Project), nameof(Project))]
    public partial class ProjectUsersEditViewModel : ViewModelBase
    {
        private readonly IUserFacade _userFacade;
        private readonly IProjectUserFacade _projectUserFacade;
        private readonly IProjectUserModelMapper _projectUserModelMapper;
        private readonly INavigationService _navigationService;

        public ProjectDetailModel? Project { get; set; }

        public ObservableCollection<UserListModel> Users { get; set; } = new();

        public UserListModel? ProjectSelected { get; set; }

        public ProjectUserListModel? ProjectUsersList { get; set; }

        public ProjectUserDetailModel? ProjectUserNew { get; private set; }

        public ProjectUsersEditViewModel(
            IUserFacade userFacade,
            IProjectUserFacade projectUserFacade,
            IProjectUserModelMapper projectUserModelMapper,
            IMessengerService messengerService,
            INavigationService navigationService)
            : base(messengerService)
        {
            _userFacade = userFacade;
            _projectUserFacade = projectUserFacade;
            _projectUserModelMapper = projectUserModelMapper;
            _navigationService = navigationService;
        }

        protected override async Task LoadDataAsync()
        {
            await base.LoadDataAsync();

            Users.Clear();
            var users = await _userFacade.GetAsync();
            foreach (var user in users)
            {
                Users.Add(user);
            }
            foreach (var user in users)
            {
                ProjectUserNew = GetProjectNew();
            }
        }
        private IAsyncRelayCommand _AddNewUser;
        public IAsyncRelayCommand AddNewUserToProject => _AddNewUser ??= new AsyncRelayCommand(AddNewUserToProjectAsync);

        [RelayCommand]
        private async Task AddNewUserToProjectAsync()
        {
            if (ProjectUserNew is not null && Project is not null)
            {
                string activeUserId = Preferences.Get("ActiveUser", defaultValue: string.Empty);
                Guid userIdGuid = Guid.Parse(activeUserId);
                var userFirst = Users.FirstOrDefault(user => user.Id == userIdGuid);

                //user is not in project already
                if (userFirst != null && !Project.Users.Any(user => user.UserId == userFirst.Id))
                {
                    await _projectUserFacade.SaveAsync(ProjectUserNew, Project.Id);

                    Project.Users.Add(_projectUserModelMapper.MapToListModel(ProjectUserNew));
                    ProjectUsersList = GetProjectUsers();

                    MessengerService.Send(new ProjectUsersEditMessage{UserId = userIdGuid, ProjectId = Project.Id});
                    _navigationService.SendBackButtonPressed();
                    _navigationService.SendBackButtonPressed();
                }
            }
        }
        //&& ProjectSelected is not null
        //_projectUserModelMapper.MapToExistingDetailModel(ProjectUserNew, ProjectSelected);
        //ProjectUserNew = GetProjectNew();

        [RelayCommand]
        private async Task UpdateUserAsync(ProjectUserListModel? model)
        {
            if (model is not null
                && Project is not null)
            {
                await _projectUserFacade.SaveAsync(model, Project.Id);

                MessengerService.Send(new UserActivitiesEditMessage());
            }
        }

        [RelayCommand]
        private async Task RemoveUserAsync(ProjectUserListModel model)
        {
            if (Project is not null)
            {
                await _userFacade.DeleteAsync(model.Id);
                Project.Users.Remove(model);

                MessengerService.Send(new UserActivitiesDeleteMessage());
            }
        }

        private ProjectUserDetailModel GetProjectNew()
        {
            string activeUserId = Preferences.Get("ActiveUser", defaultValue: string.Empty);
            Guid userIdGuid;

                userIdGuid = Guid.Parse(activeUserId);
                var userFirst = Users.FirstOrDefault(user => user.Id == userIdGuid);
                return new()
                {
                    Id = Guid.NewGuid(),
                    UserId = userFirst.Id,
                    UserName = userFirst.Name,
                    UserSurname = userFirst.Surname,
                    UserImageUrl = string.Empty,
                };
        }
        private ProjectUserListModel GetProjectUsers()
        {
            string activeUserId = Preferences.Get("ActiveUser", defaultValue: string.Empty);
            Guid userIdGuid;

            userIdGuid = Guid.Parse(activeUserId);
            var userFirst = Users.FirstOrDefault(user => user.Id == userIdGuid);
            return new()
            {
                Id = Guid.NewGuid(),
                UserId = userFirst.Id,
                UserName = userFirst.Name,
                UserSurname = userFirst.Surname,
                UserImageUrl = string.Empty,
            };
        }
    }
}
