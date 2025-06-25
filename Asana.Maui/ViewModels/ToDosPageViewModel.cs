using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Asana.Core.Models;
using Asana.Core.Services;


namespace Asana.Maui.ViewModels;

public class ToDosPageViewModel : INotifyPropertyChanged
{
    ProjectService _project;
    public ToDosPageViewModel()
    {
        _project = ProjectService.Current;
        UpdateShownToDos();
    }

    private ObservableCollection<ToDoDetailViewModel>? displayedTodos;
    public ObservableCollection<ToDoDetailViewModel> ToDos
    {
        get
        {
            return displayedTodos ?? new ObservableCollection<ToDoDetailViewModel>();
        }
        set
        {
            if (value != displayedTodos)
                displayedTodos = value;
            NotifyPropertyChanged(nameof(ToDos));
        }
    }

    public void RefreshPage()
    {
        UpdateShownToDos();
    }

    private void UpdateShownToDos()
    {
        var toDos = _project.ToDos.Select(t => new ToDoDetailViewModel(t)).Take(100);
        // If you don't want to show complete todos
        if (!IsShowCompleteToDos)
            // Show todos where IsComplete is false
            toDos = _project.ToDos.Select(t => new ToDoDetailViewModel(t)).Where(t => !t?.Model?.IsComplete ?? false).Take(100);

        ToDos = new ObservableCollection<ToDoDetailViewModel>(toDos);
    }

    private bool isShowCompleteToDos = true;
    public bool IsShowCompleteToDos
    {
        get { return isShowCompleteToDos; }
        set
        {
            if (isShowCompleteToDos != value)
            {
                isShowCompleteToDos = value;
                UpdateShownToDos();
                NotifyPropertyChanged();
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
