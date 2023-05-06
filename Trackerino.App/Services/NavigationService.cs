using Trackerino.App.Models;
using Trackerino.App.ViewModels;
using Trackerino.App.Views.User;
using Trackerino.App.Views.Project;
using ActivityDetailView = Trackerino.App.Views.Activity.ActivityDetailView;
using ActivityEditView = Trackerino.App.Views.Activity.ActivityEditView;
using ActivityListView = Trackerino.App.Views.Activity.ActivityListView;

namespace Trackerino.App.Services;

public class NavigationService : INavigationService
{
    public IEnumerable<RouteModel> Routes { get; } = new List<RouteModel>
    {
        new("//users", typeof(UserListView), typeof(UserListViewModel)),
        new("//users/detail", typeof(UserDetailView), typeof(UserDetailViewModel)),

        new("//users/edit", typeof(UserEditView), typeof(UserEditViewModel)),
        new("//users/detail/edit", typeof(UserEditView), typeof(UserEditViewModel)),

        new("//users/detail/edit/activities", typeof(UserActivitiesEditView), typeof(UserActivitiesEditViewModel)),
        new("//users/detail/edit/projects", typeof(UserProjectsEditView), typeof(UserProjectsEditViewModel)),

        new("//activities", typeof(ActivityListView), typeof(ActivityListViewModel)),
        new("//activities/detail", typeof(ActivityDetailView), typeof(ActivityDetailViewModel)),

        new("//activities/edit", typeof(ActivityEditView), typeof(ActivityEditViewModel)),
        new("//activities/detail/edit", typeof(ActivityEditView), typeof(ActivityEditViewModel)),

        new("//projects", typeof(ProjectListView), typeof(ProjectListViewModel)),
        new("//projects/detail", typeof(ProjectDetailView), typeof(ProjectDetailViewModel)),

        new("//projects/edit", typeof(ProjectEditView), typeof(ProjectEditViewModel)),
        new("//projects/detail/edit", typeof(ProjectEditView), typeof(ProjectEditViewModel)),

        new("//projects/detail/edit/users", typeof(ProjectUsersEditView), typeof(ProjectUsersEditViewModel)),
        new("//projects/detail/edit/activities", typeof(ProjectActivitiesEditView), typeof(ProjectActivitiesEditViewModel)),
    };

    public async Task GoToAsync<TViewModel>()
        where TViewModel : IViewModel
    {
        var route = GetRouteByViewModel<TViewModel>();
        await Shell.Current.GoToAsync(route);
    }
    public async Task GoToAsync<TViewModel>(IDictionary<string, object?> parameters)
        where TViewModel : IViewModel
    {
        var route = GetRouteByViewModel<TViewModel>();
        await Shell.Current.GoToAsync(route, parameters);
    }

    public async Task GoToAsync(string route)
        => await Shell.Current.GoToAsync(route);

    public async Task GoToAsync(string route, IDictionary<string, object?> parameters)
        => await Shell.Current.GoToAsync(route, parameters);

    public bool SendBackButtonPressed()
        => Shell.Current.SendBackButtonPressed();

    private string GetRouteByViewModel<TViewModel>()
        where TViewModel : IViewModel 
        => Routes.First(route => route.ViewModelType == typeof(TViewModel)).Route;
}