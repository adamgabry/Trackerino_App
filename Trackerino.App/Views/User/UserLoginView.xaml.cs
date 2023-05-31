using Trackerino.App.ViewModels;

namespace Trackerino.App.Views.User;

public partial class UserLoginView
{
    public UserLoginView(UserLoginViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}