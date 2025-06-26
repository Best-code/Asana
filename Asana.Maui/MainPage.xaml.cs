namespace Asana.Maui;

using ViewModels;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private void AddToDoClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//ToDoDetails");
	}

	private void EditToDoClicked(object sender, EventArgs e)
	{
		var ToDoId = (BindingContext as MainPageViewModel)?.SelectedToDo?.Model?.Id ?? 0;
		Shell.Current.GoToAsync($"//ToDoDetails?toDoId={ToDoId}");
	}

	private void AddProjectClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//ProjectDetails");
	}

	private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
		BindingContext = new MainPageViewModel();
		(BindingContext as MainPageViewModel)?.RefreshPage();
	}

	private void ContentPage_NavigatedFrom(object sender, NavigatedToEventArgs e)
	{
	}

}
