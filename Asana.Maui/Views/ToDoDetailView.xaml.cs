using Asana.Maui.ViewModels;

namespace Asana.Maui.Views;

[QueryProperty(nameof(ToDoId), "toDoId")]
public partial class ToDoDetailView : ContentPage
{
	public ToDoDetailView()
	{
		InitializeComponent();
		BindingContext = new ToDoDetailViewModel();
	}

	public int ToDoId { get; set; }


	private void CancelClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//MainPage");
	}

	private void AddToDoClicked(object sender, EventArgs e)
	{
		(BindingContext as ToDoDetailViewModel)?.AddToDo();
		Shell.Current.GoToAsync("//MainPage");
	}

	private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
		BindingContext = new ToDoDetailViewModel(ToDoId);
		(BindingContext as ToDoDetailViewModel).RefreshPage();
	}

	private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
	{
	}
}