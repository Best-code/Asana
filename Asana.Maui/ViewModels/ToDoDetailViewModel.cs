using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Asana.Core.Models;
using Asana.Core.Services;

namespace Asana.Maui.ViewModels;

public class ToDoDetailViewModel : INotifyPropertyChanged
{

    //TODO Even when I click Cancel it still submits the Model as Updated

    UnitService _unitSvc;
    ProjectService _projSvc;
    public ToDoDetailViewModel()
    {
        Model = new ToDo();
        InitializeViewModel();
    }

    public ToDoDetailViewModel(int toDoId)
    {
        // Passing 0 is for adding a new model / Passing an existing ID is for editing
        if (toDoId == 0)
            Model = new ToDo();
        else
            Model = ProjectService.Current.ToDos.FirstOrDefault(t => t.Id == toDoId) ?? new ToDo();
        InitializeViewModel();
    }

    public ToDoDetailViewModel(ToDo model)
    {
        Model = model;
        InitializeViewModel();
    }

    public void InitializeViewModel()
    {
        _unitSvc = UnitService.Current;
        _projSvc = ProjectService.Current;

        DeleteCommand = new Command(DoDelete);

        RefreshPage();
    }

    public void RefreshPage()
    {
        ProjectNames = new ObservableCollection<string>(_unitSvc.Projects.Select(p => p.Name));

        if (Model == null)
            Model = new();

        // If the projectName isn't in the updated list then Default to being in the first project
        SelectedProject = _unitSvc.GetProjectById(Model.ProjectId)?.Name ?? _unitSvc.Projects.First().Name;
        if (!ProjectNames.Contains(SelectedProject))
        {
            SelectedProject = ProjectNames.First();
        }

        Project? project = _unitSvc.GetProjectByName(SelectedProject);
        if (project != null)
        {
            Model.ProjectId = project.Id;
            SelectedPriority = Model.Priority;
            ProjectName = project.Name;
        }


        NotifyPropertyChanged(nameof(SelectedPriority));
        NotifyPropertyChanged(nameof(SelectedProject));
        NotifyPropertyChanged(nameof(Model));
    }


    public ICommand DeleteCommand { get; set; }
    public ToDo? Model { get; set; }

    public void AddUpdateToDo()
    {
        _projSvc.AddUpdateToDo(Model ?? new ToDo());
        Model = new();
        SelectedProject = ProjectNames.FirstOrDefault() ?? "No Projects";
        Model.Priority = 0;
    }

    public void DoDelete()
    {
        _projSvc.DeleteTodo(Model.Id);
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



    private string? projectName;
    public string ProjectName
    {
        get { return projectName ?? "No Project Name"; }
        set
        {
            if (value != projectName)
            {
                projectName = value;
                NotifyPropertyChanged();
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
                var ProjectedId = UnitService.Current.GetProjectByName(value).Id;
                selectedProject = value;
                if (Model == null)
                    Model = new();
                Model.ProjectId = ProjectedId;

                NotifyPropertyChanged(nameof(SelectedProject));
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
