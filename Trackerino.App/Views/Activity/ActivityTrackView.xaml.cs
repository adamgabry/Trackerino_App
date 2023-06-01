using Trackerino.App.ViewModels;

namespace Trackerino.App.Views.Activity;

public partial class ActivityTrackView
{
    public ActivityTrackView(ActivityTrackViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}