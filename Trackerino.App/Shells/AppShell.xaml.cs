using CommunityToolkit.Mvvm.Input;
using Trackerino.App.Services.Interfaces;
using Trackerino.App.ViewModels;

namespace Trackerino.App.Shells;

public partial class AppShell
{
    private readonly INavigationService _navigationService;

    public AppShell(INavigationService navigationService)
    {
        _navigationService = navigationService;

        InitializeComponent();
    }

    [RelayCommand]
    private async Task GoToLoginAsync()
        => await _navigationService.GoToAsync<UserLoginViewModel>();

    [RelayCommand]
    private async Task GoToUsersAsync()
        => await _navigationService.GoToAsync<UserListViewModel>();

    [RelayCommand]
    private async Task GoToProjectsAsync()
        => await _navigationService.GoToAsync<ProjectListViewModel>();

    [RelayCommand]
    private async Task GoToActivitiesAsync()
        => await _navigationService.GoToAsync<ActivityListViewModel>();
}
