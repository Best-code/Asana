using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Asana.Core.Models;
using Asana.Core.Services;

namespace Asana.Maui.ViewModels;

public class ToDoDetailViewModel : INotifyPropertyChanged
{

    UnitService _unitSvc;
    ProjectService _projSvc;
    public ToDoDetailViewModel()
    {
        Model = new ToDo();
        InitializeViewModel();
    }

    public ToDoDetailViewModel(int toDoId)
    {
        Model = ProjectService.Current.ToDos.First(t => t.Id == toDoId) ?? new ToDo();
        InitializeViewModel();
    }

    public ToDoDetailViewModel(ToDo model)
    {
        Model = model ?? new ToDo();
        InitializeViewModel();
    }

    public void InitializeViewModel()
    {
        _unitSvc = UnitService.Current;
        _projSvc = ProjectService.Current;

        RefreshPage();
    }


    public void RefreshPage()
    {
        ProjectNames = new ObservableCollection<string>(_unitSvc.Projects.Select(p => p.Name));

        if (ProjectNames.Any())
        {
            SelectedProject = ProjectNames.First();
        }
    }


    private ToDo? model;
    public ToDo? Model
    {
        get => model;
        set
        {
            if (model != value)
            {
                model = value;
                NotifyPropertyChanged(nameof(Model));
            }
        }
    }

    public void AddToDo()
    {
        _projSvc.AddTodo(Model ?? new ToDo());
        Model = new();
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


    private ObservableCollection<String>? _projectNames;
    public ObservableCollection<String> ProjectNames
    {
        get => _projectNames ?? new ObservableCollection<String>();
        private set
        {
            if (_projectNames != value)
            {
                _projectNames = value;
                NotifyPropertyChanged();
            }
        }
    }

    private string? selectedProject;
    public string SelectedProject
    {
        get => selectedProject ?? "Null Project";
        set
        {
            if (selectedProject != value)
            {
                selectedProject = value;
                if (Model != null)
                    Model.ProjectId = UnitService.Current.GetProjectByName(value).Id;
                else
                {
                    Model = new();
                    Model.ProjectId = UnitService.Current.GetProjectByName(value).Id;
                }

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
