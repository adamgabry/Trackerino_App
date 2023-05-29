using Trackerino.App.ViewModels;

namespace Trackerino.App.Views.User;

public partial class UserEditView
{
    public UserEditView(UserEditViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}