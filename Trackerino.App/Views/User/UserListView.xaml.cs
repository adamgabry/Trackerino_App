using Trackerino.App.ViewModels;

namespace Trackerino.App.Views.User;

public partial class UserListView
{
	public UserListView(UserListViewModel viewModel)
        : base(viewModel)
    {
		InitializeComponent();
	}
}
