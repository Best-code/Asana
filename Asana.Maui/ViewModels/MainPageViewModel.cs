using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Asana.Core.Models;
using System.Threading.Tasks;


namespace Asana.Maui.ViewModels;

public class MainPageViewModel : INotifyPropertyChanged
{
    Project _project;
    public MainPageViewModel()
    {
        _project = new("Project One", "This is my first project");
    }

    public ObservableCollection<ToDo> ToDos
    {
        get
        {
            List<ToDo> todos;
            // if (IsShowCompleteToDos)
            //     todos = _project.ToDos;
            // else
            //     todos = _project.ToDos.Where(t => !t.IsComplete).ToList();

            todos = _project.ToDos;

            return new ObservableCollection<ToDo>(todos);
        }
    }

    private bool isShowCompleteToDos;
    public bool IsShowCompleteToDos
    {
        get { return isShowCompleteToDos; }
        set
        {
            if (isShowCompleteToDos != value)
            {
                isShowCompleteToDos = value;
                NotifyPropertyChanged(nameof(ToDos));
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
