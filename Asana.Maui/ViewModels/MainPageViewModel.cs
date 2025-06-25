using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Asana.Core.Models;
using Asana.Core.Services;


namespace Asana.Maui.ViewModels;

public class MainPageViewModel : INotifyPropertyChanged
{
    ProjectService _projSvc;
    UnitService _unitSvc;
    public MainPageViewModel()
    {
        _projSvc = ProjectService.Current;
        _unitSvc = UnitService.Current;

        // ProjectNames = new ObservableCollection<string>([] _unitSvc.Projects.Select(p => p.Name));
        ProjectNames = new ObservableCollection<string> { "All" };
        foreach (Project p in _unitSvc.Projects)
        {
            ProjectNames.Add(p.Name);
        }

        if (ProjectNames != null && ProjectNames.Any())
            SelectedProject = ProjectNames.First();
    }

    public ToDo? Model { get; set; } = new();

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
                NotifyPropertyChanged();
                UpdateShownProjects();
            }
        }
    }


    private bool? isShowCompleteToDos;
    public bool IsShowCompleteToDos
    {
        get { return isShowCompleteToDos ?? true; }
        set
        {
            if (isShowCompleteToDos != value)
            {
                isShowCompleteToDos = value;
                UpdateShownProjects();
                NotifyPropertyChanged();
            }
        }
    }

    private ObservableCollection<ToDoViewModel>? displayedToDos;
    public ObservableCollection<ToDoViewModel> ToDos
    {
        get
        {
            return displayedToDos ?? new ObservableCollection<ToDoViewModel>();
        }
        set
        {
            if (value != displayedToDos)
            {
                displayedToDos = value;
                NotifyPropertyChanged(nameof(ToDos));
            }
        }
    }

    public void RefreshPage()
    {
         ProjectNames = new ObservableCollection<string> { "All" };
        foreach (Project p in _unitSvc.Projects)
        {
            ProjectNames.Add(p.Name);
        }

        if (ProjectNames != null && ProjectNames.Any())
            SelectedProject = ProjectNames.First();
    }

    private void UpdateShownProjects()
    {
        var toDos = _projSvc.ToDos.Select(t => new ToDoViewModel(t)).Take(100);
        // If you don't want to show complete projects
        if (!IsShowCompleteToDos)
            // Show todos where IsComplete is not true
            toDos = _projSvc.ToDos.Select(t => new ToDoViewModel(t)).Where(t => !t?.Model?.IsComplete ?? false).Take(100);


        // Only get ToDos in selected project
        if (SelectedProject != null && SelectedProject != "All")
        {
            int selectedId = _unitSvc.GetProjectByName(SelectedProject).Id;
            toDos = toDos.Where(t => t?.Model?.ProjectId == selectedId);
        }

        ToDos = new ObservableCollection<ToDoViewModel>(toDos);
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
