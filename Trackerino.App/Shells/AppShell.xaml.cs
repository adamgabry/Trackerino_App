using CommunityToolkit.Mvvm.Input;
using Trackerino.App.ViewModels;
using Trackerino.App.Services;

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
    private async Task GoToActivityList()
        => await _navigationService.GoToAsync<ActivityListViewModel>();

    [RelayCommand]
    private async Task GoToProjectList()
        => await _navigationService.GoToAsync<ProjectListViewModel>();

    [RelayCommand]
    private async Task GoToUsersList()
        => await _navigationService.GoToAsync<UserListViewModel>();
}