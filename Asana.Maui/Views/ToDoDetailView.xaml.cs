using Asana.Maui.ViewModels;

namespace Asana.Maui.Views;

[QueryProperty(nameof(ToDoId), "toDoId")]
public partial class ToDoDetailView : ContentPage
{
	public ToDoDetailView()
	{
		InitializeComponent();
	}

	private int toDoId = -1;
	public int ToDoId
	{
		get => toDoId;
		set
		{
			if (value != toDoId)
			{
				// Make sure that the toDoId is set first so that all the data is synced first and then loads properly
				toDoId = value;
				BindingContext = new ToDoDetailViewModel(ToDoId);
			}
		}
	}


	private void CancelClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//MainPage");
	}

	private void SubmitClicked(object sender, EventArgs e)
	{
		(BindingContext as ToDoDetailViewModel)?.AddUpdateToDo();
		Shell.Current.GoToAsync("//MainPage");
	}

	private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
	}

	private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
	{
		// Reset to defaults when you leave
		toDoId = -1;
	}
}