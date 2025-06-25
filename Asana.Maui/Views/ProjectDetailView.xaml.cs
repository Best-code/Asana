using Asana.Core.Models;
using Asana.Maui.ViewModels;

namespace Asana.Maui.Views;

[QueryProperty(nameof(ProjectId), "projectId")]
public partial class ProjectDetailView : ContentPage
{
	public ProjectDetailView()
	{
		InitializeComponent();
		BindingContext = new ProjectDetailViewModel();
	}

	public int ProjectId {get; set;}
	
	private void CancelClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//MainPage");
	}

	private void AddProjectClicked(object sender, EventArgs e)
	{
		(BindingContext as ProjectDetailViewModel)?.AddProject();
		Shell.Current.GoToAsync("//MainPage");
	}

	private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
		BindingContext = new ProjectDetailViewModel(ProjectId);
		(BindingContext as ProjectDetailViewModel).RefreshPage();
	}

	private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
	{
	}
}