namespace Asana.Maui;

using ViewModels;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
		BindingContext = new MainPageViewModel();
	}

	private void AddToDoClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//ToDoDetails");
	}

	private void AddProjectClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//ProjectDetails");
	}

	private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
		(BindingContext as MainPageViewModel)?.RefreshPage();
	}

	private void ContentPage_NavigatedFrom(object sender, NavigatedToEventArgs e)
	{
	}

}
