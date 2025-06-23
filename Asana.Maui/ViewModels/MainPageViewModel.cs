using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Asana.Core.Models;


namespace Asana.Maui.ViewModels;

public class MainPageViewModel : INotifyPropertyChanged
{
    Project _project;
    public MainPageViewModel()
    {
        _project = new("Project One", "This is my first project");
        UpdateShownToDos();
    }

    private ObservableCollection<ToDo> displayedTodos;
    public ObservableCollection<ToDo> ToDos
    {
        get
        {
            return new ObservableCollection<ToDo>(displayedTodos);
        }
        set
        {
            if (value != displayedTodos)
                displayedTodos = value;
            NotifyPropertyChanged(nameof(ToDos));
        }
    }

    private void UpdateShownToDos()
    {
        var todos = isShowCompleteToDos ?
            _project.ToDos :
            _project.ToDos.Where(t => !t.IsComplete).ToList();

        ToDos = new ObservableCollection<ToDo>(todos);
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
