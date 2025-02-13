﻿using Trackerino.App.Models;
using Trackerino.App.ViewModels;
using Trackerino.App.Views.Activity;
using Trackerino.App.Views.Project;
using Trackerino.App.Views.User;

namespace Trackerino.App.Services
{
    public class NavigationService : INavigationService
    {
        public IEnumerable<RouteModel> Routes { get; } = new List<RouteModel>
        {
            new("//login", typeof(UserLoginView), typeof(UserLoginViewModel)),

            new("//users", typeof(UserListView), typeof(UserListViewModel)),
            new("//users/detail", typeof(UserDetailView), typeof(UserDetailViewModel)),

            new("//users/edit", typeof(UserEditView), typeof(UserEditViewModel)),
            new("//users/detail/edit", typeof(UserEditView), typeof(UserEditViewModel)),

            new("//activities", typeof(ActivityListView), typeof(ActivityListViewModel)),
            new("//activities/detail", typeof(ActivityDetailView), typeof(ActivityDetailViewModel)),

            new("//activities/track", typeof(ActivityTrackView), typeof(ActivityTrackViewModel)),

            new("//activities/edit", typeof(ActivityEditView), typeof(ActivityEditViewModel)),
            new("//activities/detail/edit", typeof(ActivityEditView), typeof(ActivityEditViewModel)),

            new("//projects", typeof(ProjectListView), typeof(ProjectListViewModel)),
            new("//projects/detail", typeof(ProjectDetailView), typeof(ProjectDetailViewModel)),

            new("//projects/edit", typeof(ProjectEditView), typeof(ProjectEditViewModel)),
            new("//projects/detail/edit", typeof(ProjectEditView), typeof(ProjectEditViewModel)),

            new("//projects/detail/edit/users", typeof(ProjectUsersEditView), typeof(ProjectUsersEditViewModel)),
        };

        public async Task GoToAsync<TViewModel>()
            where TViewModel : IViewModel
        {
            string route = GetRouteByViewModel<TViewModel>();
            await Shell.Current.GoToAsync(route);
        }

        public async Task GoToAsync<TViewModel>(IDictionary<string, object?> parameters)
            where TViewModel : IViewModel
        {
            string route = GetRouteByViewModel<TViewModel>();
            await Shell.Current.GoToAsync(route, parameters);
        }

        public async Task GoToAsync(string route)
        {
            await Shell.Current.GoToAsync(route);
        }

        public async Task GoToAsync(string route, IDictionary<string, object?> parameters)
        {
            await Shell.Current.GoToAsync(route, parameters);
        }

        public bool SendBackButtonPressed()
        {
            return Shell.Current.SendBackButtonPressed();
        }

        private string GetRouteByViewModel<TViewModel>()
            where TViewModel : IViewModel
        {
            return Routes.First(route => route.ViewModelType == typeof(TViewModel)).Route;
        }
    }
}