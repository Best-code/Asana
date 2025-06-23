using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Asana.Core.Models;
using Asana.Core.Services;

namespace Asana.Maui.ViewModels;

public class ToDoDetailViewModel : INotifyPropertyChanged
{
    public ToDoDetailViewModel()
    {
        Model = new ToDo();
    }
    public ToDo? Model { get; set; }

    public void AddToDo()
    {
    }

    public List<int> Priorities
    {
        get
        {
            return new List<int> { 0, 1, 2, 3, 4 };
        }
    }

    public int SelectedPriority
    {
        get
        {
            return Model?.Priority ?? 0;
        }
        set
        {
            if (Model != null && Model.Priority != value)
            {
                Model.Priority = value;
                NotifyPropertyChanged(nameof(SelectedPriority));
            }
        }
    } 

    public event PropertyChangedEventHandler? PropertyChanged;
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
