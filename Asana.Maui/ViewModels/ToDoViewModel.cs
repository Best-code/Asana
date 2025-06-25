using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Asana.Core.Models;
using Asana.Core.Services;

namespace Asana.Maui.ViewModels;

public class ToDoViewModel : INotifyPropertyChanged
{

    UnitService _unitSvc;
    public ToDoViewModel()
    {
        Model = new ToDo();
        InitializeViewModel();
    }

    public ToDoViewModel(int toDoId)
    {
        Model = ProjectService.Current.ToDos.FirstOrDefault(t => t.Id == toDoId) ?? new ToDo();
        InitializeViewModel();
    }

    public ToDoViewModel(ToDo model)
    {
        Model = model ?? new ToDo();
        InitializeViewModel();
    }

    public void InitializeViewModel()
    {
        _unitSvc = UnitService.Current;
        ProjectName = _unitSvc.GetProjectById(Model.ProjectId).Name;
    }

    public ToDo? Model { get; set; }


    private string? projectName;
    public string ProjectName
    {
        get { return projectName; }
        set
        {
            if (value != projectName)
            {
                projectName = value;
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
