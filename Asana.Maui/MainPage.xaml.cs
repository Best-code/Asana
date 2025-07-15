namespace Asana.Maui;

using Asana.Core.Models;
using Asana.Core.Services;
using ViewModels;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private void AddToDoClicked(object sender, EventArgs e)
	{
		// You must have atleast 1 projectName other than "All" to be able to add a toDo
		int? projectCount = (BindingContext as MainPageViewModel)?.ProjectNames.Count();
		if (projectCount != null && projectCount > 1)
		{
			Shell.Current.GoToAsync("//ToDoDetails?toDoId=0");
		}
	}

	private void EditToDoClicked(object sender, EventArgs e)
	{
		var ToDoId = (BindingContext as MainPageViewModel)?.SelectedToDo?.Model?.Id ?? 0;
		if (ToDoId != 0)
			Shell.Current.GoToAsync($"//ToDoDetails?toDoId={ToDoId}");
	}

	private void DeleteToDoClicked(object sender, EventArgs e)
	{
		ToDoDetailViewModel? toDo = (BindingContext as MainPageViewModel)?.SelectedToDo;
		if (toDo != null)
			(BindingContext as MainPageViewModel)?.DeleteToDo(toDo.Model.Id);
	}

	private void DeleteProjectClicked(object sender, EventArgs e)
	{
		string selectedProj = (BindingContext as MainPageViewModel)?.SelectedProject;
		if (selectedProj != "All" && selectedProj != null)
		{
			Project proj = UnitService.Current.GetProjectByName(selectedProj);
			if (proj != null)
				(BindingContext as MainPageViewModel)?.DeleteProject(proj);
		}
	}

	private void AddProjectClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//ProjectDetails?projectId=0");
	}

	private void EditProjectClicked(object sender, EventArgs e)
	{
		Project? project = UnitService.Current.GetProjectByName((BindingContext as MainPageViewModel).SelectedProject);

		//If the selectedProj exist and isn't the "All" option
		if (project != null && project.Name != "All")
		{
			var ProjectId = project.Id;
			if (ProjectId > 0)
				Shell.Current.GoToAsync($"//ProjectDetails?projectId={ProjectId}");
		}

	}

	private void InlineDeleteClicked(object sender, EventArgs e)
	{
		(BindingContext as MainPageViewModel)?.InlineDeleteClicked();
	}

	private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
		BindingContext = new MainPageViewModel();
		(BindingContext as MainPageViewModel)?.RefreshPage();
	}

	private void ContentPage_NavigatedFrom(object sender, NavigatedToEventArgs e)
	{
	}

	private void SearchClicked(object sender, EventArgs e)
	{
		(BindingContext as MainPageViewModel)?.SearchQuery();
    }

}
