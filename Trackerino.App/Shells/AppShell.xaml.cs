using CommunityToolkit.Mvvm.Input;
using Trackerino.App.Services;
using Trackerino.App.ViewModels;

namespace Trackerino.App.Shells;

public partial class AppShell
{
    private readonly INavigationService _navigationService;
    private readonly IAlertService _alertService;

    public AppShell(INavigationService navigationService, IAlertService alertService)
    {
        _navigationService = navigationService;
        _alertService = alertService;
        InitializeComponent();
    }

    [RelayCommand]
    private async Task GoToLoginAsync()
    {
        Preferences.Set("ActiveUser", Guid.Empty.ToString());
        await _navigationService.GoToAsync<UserLoginViewModel>();
    }

    [RelayCommand]
    private async Task GoToUsersAsync()
    {
        if (Guid.Parse(Preferences.Get("ActiveUser", string.Empty)) == Guid.Empty)
        {
            await _alertService.DisplayAsync("Failed to login", "You must first login");
        }
        else
        {
            await _navigationService.GoToAsync<UserListViewModel>();
        }
    }

    [RelayCommand]
    private async Task GoToProjectsAsync()
    {
        if (Guid.Parse(Preferences.Get("ActiveUser", string.Empty)) == Guid.Empty)
        {
            await _alertService.DisplayAsync("Failed to login", "You must first login");
        }
        else
        {
            await _navigationService.GoToAsync<ProjectListViewModel>();
        }
    }

    [RelayCommand]
    private async Task GoToActivitiesAsync()
    {
        if (Guid.Parse(Preferences.Get("ActiveUser", string.Empty)) == Guid.Empty)
        {
            await _alertService.DisplayAsync("Failed to login", "You must first login");
        }
        else
        {
            await _navigationService.GoToAsync<ActivityListViewModel>();
        }

    }
}
