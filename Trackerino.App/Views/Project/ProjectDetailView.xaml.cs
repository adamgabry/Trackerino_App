using Trackerino.App.ViewModels;

namespace Trackerino.App.Views.Project;

public partial class ProjectDetailView
{
    public ProjectDetailView(ProjectDetailViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}