using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackerino.App.Messages;
using Trackerino.App.Services.Interfaces;
using Trackerino.BL.Facades.Interfaces;
using Trackerino.BL.Mappers.Interfaces;
using Trackerino.BL.Models;
using Trackerino.DAL.Common;

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
            this._projectFacade = projectFacade;
            this._userProjectFacade = userProjectFacade;
            this._userProjectModelMapper = userProjectModelMapper;
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
