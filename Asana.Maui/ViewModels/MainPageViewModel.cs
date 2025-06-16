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
            var todos = _project.ToDos;
            // if (IsShowCompleteProjects)
            // {
            //     projects = _unit.Projects;
            // }
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
                NotifyPropertyChanged();
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
