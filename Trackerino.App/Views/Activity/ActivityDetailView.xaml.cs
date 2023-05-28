using Trackerino.App.ViewModels;

namespace Trackerino.App.Views.Activity;

public partial class ActivityDetailView
{
    public ActivityDetailView(ActivityDetailViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}