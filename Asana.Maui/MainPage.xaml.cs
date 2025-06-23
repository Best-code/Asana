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

}
