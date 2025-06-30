using Asana.Core.Models;
using Asana.Maui.ViewModels;

namespace Asana.Maui.Views;

[QueryProperty(nameof(ProjectId), "projectId")]
public partial class ProjectDetailView : ContentPage
{
	public ProjectDetailView()
	{
		InitializeComponent();
	}

	private int projectId;
	public int ProjectId
	{
		get => projectId;
		set
		{
			if (value != projectId)
			{
				projectId = value;
				BindingContext = new ProjectDetailViewModel(ProjectId);
			}
		}
	}

	private void CancelClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//MainPage");
	}

	private void SubmitClicked(object sender, EventArgs e)
	{
		(BindingContext as ProjectDetailViewModel).AddUpdateProject();
		Shell.Current.GoToAsync("//MainPage");
	}

	private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
		// BindingContext = new ProjectDetailViewModel(ProjectId);
	}

	private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
	{
	}
}